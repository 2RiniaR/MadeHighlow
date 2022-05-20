using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityTeleport
{
    public record TargetNotFoundResult([NotNull] EntityTeleportAction Action) : EntityTeleportResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
