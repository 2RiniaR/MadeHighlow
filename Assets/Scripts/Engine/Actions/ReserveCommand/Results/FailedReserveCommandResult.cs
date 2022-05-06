using System;

namespace RineaR.MadeHighlow
{
    public record FailedReserveCommandResult(ReserveCommandFailedReason Code) : ReserveCommandResult
    {
        public override World Simulate(in World world)
        {
            throw new InvalidOperationException("The failed event could not simulate.");
        }
    }
}