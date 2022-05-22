using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record NotFoundResult([NotNull] DropCardAction Action) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
