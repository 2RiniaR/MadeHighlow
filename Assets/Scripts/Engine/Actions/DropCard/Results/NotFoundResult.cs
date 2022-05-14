using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record NotFoundResult([NotNull] CardID TargetID) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
