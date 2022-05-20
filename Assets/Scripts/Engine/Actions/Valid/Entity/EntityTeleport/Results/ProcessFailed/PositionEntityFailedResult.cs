using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PositionEntity;

namespace RineaR.MadeHighlow.Actions.Valid.EntityTeleport
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
