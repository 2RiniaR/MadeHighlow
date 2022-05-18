using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public record NotFoundResult([NotNull] DropCardAction Action) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
