using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.SupplyCard.RegisterCard;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record RegisterFailedResult(
        [NotNull] PlayerID TargetID,
        [NotNull] Card InitialStatus,
        [NotNull] RegisterCardResult FailedResult
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
