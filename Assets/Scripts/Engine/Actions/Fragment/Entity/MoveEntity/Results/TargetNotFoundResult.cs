using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
{
    public record TargetNotFoundResult([NotNull] MoveEntityAction Action) : MoveEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
