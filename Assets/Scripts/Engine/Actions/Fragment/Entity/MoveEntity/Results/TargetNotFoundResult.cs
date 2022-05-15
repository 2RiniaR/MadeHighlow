using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
{
    public record TargetNotFoundResult([NotNull] EntityID TargetID) : MoveEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
