using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard.PutCard
{
    public record SucceedResult([NotNull] Card Positioned) : PutCardResult
    {
        public override World Simulate(World world)
        {
            return Positioned.UpdateIn(world);
        }
    }
}
