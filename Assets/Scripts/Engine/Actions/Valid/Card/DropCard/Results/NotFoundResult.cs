using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public record NotFoundResult([NotNull] CardID TargetID) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
