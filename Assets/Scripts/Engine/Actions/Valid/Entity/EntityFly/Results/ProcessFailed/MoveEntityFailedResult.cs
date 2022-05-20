using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.MoveEntity;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public record MoveEntityFailedResult(EntityFlyAction Action, [NotNull] MoveEntityResult Failed) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
