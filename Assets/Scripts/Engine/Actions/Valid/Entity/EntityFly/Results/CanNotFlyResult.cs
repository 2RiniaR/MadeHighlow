using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public record CanNotFlyResult([NotNull] EntityID TargetID) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
