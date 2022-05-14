using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public class DestroyTileEvaluator
    {
        public DestroyTileEvaluator([NotNull] IHistory history, [NotNull] TileID targetID)
        {
            History = history;
            TargetID = targetID;
        }

        [NotNull] private IHistory History { get; set; }
        [NotNull] private TileID TargetID { get; }

        [CanBeNull] private ValueList<ReactedResult<RemoveComponent.SucceedResult>> RemoveComponentResults { get; set; }
        [CanBeNull] private ValueList<Interrupt<DestroyTileEffect>> Interrupts { get; set; }
        [CanBeNull] private Tile Target { get; set; }

        [NotNull]
        public DestroyTileResult Evaluate()
        {
            DestroyTileResult result;

            result = GetTarget();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            result = CheckRemainingEntity();
            if (result != null) return result;

            result = RemoveAttachedComponents();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private DestroyTileResult GetTarget()
        {
            Contract.Ensures((Contract.Result<DestroyTileResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(History.World);
            if (Target == null)
            {
                return new NotFoundResult(TargetID);
            }

            return null;
        }

        [CanBeNull]
        private DestroyTileResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IDestroyTileEffector>(History.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnDestroyTile(History, Target)).Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Target, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [CanBeNull]
        private DestroyTileResult CheckRemainingEntity()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            var removable = new EntityCondition(Target.Position2D).Search(History.World).IsEmpty;
            if (!removable)
            {
                return new EntityRemainingResult(Target, Interrupts);
            }

            return null;
        }

        [CanBeNull]
        private DestroyTileResult RemoveAttachedComponents()
        {
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(RemoveComponentResults != null);

            RemoveComponentResults = ValueList<ReactedResult<RemoveComponent.SucceedResult>>.Empty;
            foreach (var component in Target.Components)
            {
                var result = new RemoveComponentAction(component.ComponentID).Evaluate(History);
                var succeedResult = result.BodyAs<RemoveComponent.SucceedResult>();
                if (succeedResult == null)
                {
                    return new RemoveComponentFailedResult(Target, Interrupts, RemoveComponentResults, result);
                }

                History = History.Appended(succeedResult);
                RemoveComponentResults = RemoveComponentResults.Add(succeedResult);
            }

            return null;
        }

        [NotNull]
        private DestroyTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RemoveComponentResults != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(Target != null);

            return new SucceedResult(Target, RemoveComponentResults, Interrupts);
        }
    }
}
