using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record RejectedAddComponentResult(
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
