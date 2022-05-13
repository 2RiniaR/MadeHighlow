using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public class DestroyTileEvaluator
    {
        public DestroyTileEvaluator([NotNull] IActionContext context, [NotNull] TileID targetID)
        {
            Context = context;
            TargetID = targetID;
        }

        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private TileID TargetID { get; }

        [CanBeNull] private ValueList<RemoveComponent.SucceedResult> RemoveComponentResults { get; set; }
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

            result = RemoveComponents();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private DestroyTileResult GetTarget()
        {
            Contract.Ensures((Contract.Result<DestroyTileResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(Context.World);
            if (Target == null)
            {
                return new NotFoundResult(TargetID);
            }

            return null;
        }

        [CanBeNull]
        private DestroyTileResult CollectInterrupts()
        {
            Contract.Requires<ArgumentNullException>(Target != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IDestroyTileEffector>(Context.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnDestroyTile(Context, Target)).Sort();
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
            Contract.Requires<ArgumentNullException>(Target != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            var removable = new EntityCondition(Target.Position2D).Search(Context.World).IsEmpty;
            if (!removable)
            {
                return new EntityRemainingResult(Target, Interrupts);
            }

            return null;
        }

        [CanBeNull]
        private DestroyTileResult RemoveComponents()
        {
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<ArgumentNullException>(Target != null);
            Contract.Ensures(RemoveComponentResults != null);

            RemoveComponentResults = ValueList<RemoveComponent.SucceedResult>.Empty;
            foreach (var component in Target.Components)
            {
                var result = new RemoveComponentAction(component.ComponentID).Evaluate(Context);
                if (result is not RemoveComponent.SucceedResult succeedResult)
                {
                    return new RemoveComponentFailedResult(Target, Interrupts, RemoveComponentResults, result);
                }

                Context = Context.Appended(succeedResult);
                RemoveComponentResults = RemoveComponentResults.Add(succeedResult);
            }

            return null;
        }

        [NotNull]
        private DestroyTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RemoveComponentResults != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<ArgumentNullException>(Target != null);

            return new SucceedResult(Target, RemoveComponentResults, Interrupts);
        }
    }
}
