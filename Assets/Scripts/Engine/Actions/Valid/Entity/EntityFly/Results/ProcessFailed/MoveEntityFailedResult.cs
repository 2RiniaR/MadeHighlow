using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.MoveEntity;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record MoveEntityFailedResult(EntityFlyAction Action, [NotNull] MoveEntityResult Failed) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
