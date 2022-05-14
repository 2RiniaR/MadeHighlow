using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record NotFoundResult([NotNull] CardID TargetID) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
