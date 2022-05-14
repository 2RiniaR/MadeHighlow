using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PutCard
{
    public record ReplacedResult(
        [NotNull] Card Positioned,
        [NotNull] ReactedResult<Valid.DropCard.SucceedResult> DropCardResult
    ) : SucceedResult(Positioned)
    {
        public override World Simulate(World world)
        {
            return Positioned.UpdateIn(world);
        }
    }
}
