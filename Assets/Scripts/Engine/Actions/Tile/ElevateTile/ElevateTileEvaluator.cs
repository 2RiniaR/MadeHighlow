using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public class ElevateTileEvaluator
    {
        public ElevateTileEvaluator(
            [NotNull] IHistory context,
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

        [NotNull] private IHistory Context { get; }
        private ID SourceID { get; }
        [NotNull] private TileID TargetID { get; }
        [NotNull] private Elevate Expected { get; }
        [CanBeNull] private Tile Target { get; set; }

        [CanBeNull] private ValueList<Interrupt<ElevateTileEffect>> Interrupts { get; set; }

        [NotNull]
        public ElevateTileResult Evaluate()
        {
            ElevateTileResult result;

            result = GetTarget();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private ElevateTileResult GetTarget()
        {
            Contract.Ensures((Contract.Result<ElevateTileResult>() != null) ^ (Target != null));

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
            Contract.Ensures(Interrupts != null);

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
