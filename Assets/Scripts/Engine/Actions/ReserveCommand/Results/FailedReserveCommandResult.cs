using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record FailedReserveCommandResult
        ([NotNull] Command Command, FailedReserveCommandReason Code) : ReserveCommandResult
    {
        public override World Simulate(World world)
        {
            throw new InvalidOperationException("The failed event could not simulate.");
        }
    }
}
