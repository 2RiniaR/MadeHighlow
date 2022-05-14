using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record NotFoundResult([NotNull] ComponentID TargetID) : RemoveComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
