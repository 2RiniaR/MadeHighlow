using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterCard;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
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
