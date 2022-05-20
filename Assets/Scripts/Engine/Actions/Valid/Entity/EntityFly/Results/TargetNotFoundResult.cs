using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public record TargetNotFoundResult([NotNull] EntityFlyAction Action) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
