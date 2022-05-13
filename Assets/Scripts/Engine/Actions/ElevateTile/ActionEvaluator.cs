using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public class ActionEvaluator
    {
        public ActionEvaluator(
            [NotNull] IActionContext context,
            ID sourceID,
            [NotNull] TileID targetID,
            [NotNull] Elevate expected
        )
        {
            Context = context;
            SourceID = sourceID;
            TargetID = targetID;
            Expected = expected;
        }

        [NotNull] private IActionContext Context { get; }
        private ID SourceID { get; }
        [NotNull] private TileID TargetID { get; }
        [NotNull] private Elevate Expected { get; }
        [CanBeNull] private Tile Target { get; set; }

        [CanBeNull] private ValueList<Interrupt<ElevateTileEffect>> Interrupts { get; set; }

        [NotNull]
        public ElevateTileResult Evaluate()
        {
            Contract.Ensures(Contract.Result<ElevateTileResult>() != null);

            ElevateTileResult error;

            error = GetTarget();
            if (error != null) return error;

            error = CollectInterrupts();
            if (error != null) return error;

            return Succeed();
        }

        [CanBeNull]
        private ElevateTileResult GetTarget()
        {
            Target = TargetID.GetFrom(Context.World);
            if (Target == null)
            {
                return new NotFoundResult(TargetID);
            }

            return null;
        }

        [CanBeNull]
        private ElevateTileResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Target != null);

            var effectors = Component.GetAllOfTypeFrom<IElevateTileEffector>(Context.World);
            Interrupts = effectors.SelectMany(
                    effector => effector.EffectsOnElevateTile(Context, SourceID, Target, Expected)
                )
                .Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(SourceID, Target, Expected, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [NotNull]
        private ElevateTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(SourceID, Target, Expected, Interrupts, Expected);
        }
    }
}
