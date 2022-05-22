using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.PositionEntity;

namespace RineaR.MadeHighlow.Actions.EntityTeleport
{
    public record PositionEntityFailedResult(
        [NotNull] EntityTeleportAction Action,
        [NotNull] PositionEntityResult Failed
    ) : EntityTeleportResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
