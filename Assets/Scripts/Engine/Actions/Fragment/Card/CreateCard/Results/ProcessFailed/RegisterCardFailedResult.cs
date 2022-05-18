using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterCard;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateCard
{
    public record RegisterCardFailedResult(
        [NotNull] CreateCardAction Action,
        [NotNull] RegisterCardResult Failed
    ) : CreateCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
