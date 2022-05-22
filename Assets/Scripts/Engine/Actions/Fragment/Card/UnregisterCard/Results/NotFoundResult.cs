using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterCard
{
    public record NotFoundResult([NotNull] UnregisterCardAction Action) : UnregisterCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
