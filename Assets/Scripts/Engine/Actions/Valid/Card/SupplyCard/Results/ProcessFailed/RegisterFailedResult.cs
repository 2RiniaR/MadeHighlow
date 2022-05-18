using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterCard;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public record RegisterFailedResult(
        [NotNull] SupplyCardAction Action,
        [NotNull] RegisterCardResult Failed
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
