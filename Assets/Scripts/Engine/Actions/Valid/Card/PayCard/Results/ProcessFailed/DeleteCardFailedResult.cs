using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteCard;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public record DeleteCardFailedResult([NotNull] PayCardAction Action, [NotNull] DeleteCardResult Failed) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
