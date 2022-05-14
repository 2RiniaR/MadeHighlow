using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.PutCard
{
    public record ReplacedResult(
        [NotNull] Card Positioned,
        [NotNull] ReactedResult<Actions.DropCard.SucceedResult> DropCardResult
    ) : SucceedResult(Positioned)
    {
        public override World Simulate(World world)
        {
            return Positioned.UpdateIn(world);
        }
    }
}
