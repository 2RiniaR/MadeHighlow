using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityTeleport
{
    public record EntityTeleportAction([NotNull] EntityID TargetID, [NotNull] Position3D Destination) : IValidAction;
}
