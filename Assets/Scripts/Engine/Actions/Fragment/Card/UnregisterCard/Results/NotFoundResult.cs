using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.UnregisterCard
{
    public record NotFoundResult([NotNull] UnregisterCardAction Action) : UnregisterCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
