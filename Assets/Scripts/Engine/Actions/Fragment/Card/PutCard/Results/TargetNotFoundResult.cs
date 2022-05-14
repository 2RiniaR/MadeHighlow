using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PutCard
{
    public record TargetNotFoundResult([NotNull] CardID TargetID) : PutCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
