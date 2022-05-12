using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record RejectedResult([NotNull] Component Target, [NotNull] ComponentID RejectedID) : RemoveComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
