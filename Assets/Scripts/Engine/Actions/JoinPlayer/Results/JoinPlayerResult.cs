using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record JoinPlayerResult : Result
    {
        [NotNull] public RegisterPlayerResult RegisterPlayerResult { get; init; } = new();
        [NotNull] public SupplyCardResult SupplyCardResult { get; init; } = new SucceedSupplyCardResult();
        [NotNull] public ValueObjectList<AddComponentResult> AddComponentResults { get; init; } = new();

        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}