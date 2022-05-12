using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record RejectedResult(
        [NotNull] Component Component,
        [NotNull] ComponentID RejectedComponentID
    ) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
