using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RegisterCard;

namespace RineaR.MadeHighlow.Actions.CreateCard
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
