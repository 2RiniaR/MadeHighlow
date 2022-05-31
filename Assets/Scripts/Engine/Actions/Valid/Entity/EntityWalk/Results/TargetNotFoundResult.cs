using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public record TargetNotFoundResult([NotNull] EntityWalkAction Action) : EntityWalkResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
