using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public record TargetNotFoundResult([NotNull] EntityID TargetID) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
