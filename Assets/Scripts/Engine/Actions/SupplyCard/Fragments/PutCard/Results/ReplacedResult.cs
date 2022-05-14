using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard.PutCard
{
    public record ReplacedResult(
        [NotNull] Card Positioned,
        [NotNull] ReactedResult<DropCard.SucceedResult> DropCardResult
    ) : SucceedResult(Positioned)
    {
        public override World Simulate(World world)
        {
            return Positioned.UpdateIn(world);
        }
    }
}
