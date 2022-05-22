using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record CanNotFlyResult([NotNull] EntityFlyAction Action) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
