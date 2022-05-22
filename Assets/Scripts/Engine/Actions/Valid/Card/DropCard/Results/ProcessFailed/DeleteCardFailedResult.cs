using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteCard;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record DeleteCardFailedResult(
        [NotNull] DropCardAction Action,
        [NotNull] DeleteCardResult Failed
    ) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
