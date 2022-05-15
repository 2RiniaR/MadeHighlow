using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityTeleport
{
    public record EntityTeleportAction
        ([NotNull] EntityID TargetID, [NotNull] Position3D Destination) : ValidAction<EntityTeleportResult>
    {
        protected override EntityTeleportResult EvaluateBody(IHistory history)
        {
            return new EntityTeleportEvaluator(history, TargetID, Destination).Evaluate();
        }
    }
}
