using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public record ElevateTileAction
        (ID SourceID, [NotNull] TileID TargetID, [NotNull] Elevate Elevate) : Action<ElevateTileResult>
    {
        public override ElevateTileResult Evaluate(IActionContext context)
        {
            var target = TargetID.GetFrom(context.World);
            if (target == null)
            {
                return new NotFoundResult(TargetID);
            }

            var effectors = Component.GetAllOfTypeFrom<IElevateTileEffector>(context.World);
            var interrupts = effectors
                .SelectMany(effector => effector.EffectsOnInstantDeath(context, SourceID, target, Elevate))
                .Sort();
            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(SourceID, target, interrupts, interrupt.ComponentID);
                }
            }

            var modifiedTarget = target with { Elevation = Elevate.Caused(target.Elevation) };
            return new SucceedResult(SourceID, target, Elevate, interrupts, modifiedTarget);
        }
    }
}
