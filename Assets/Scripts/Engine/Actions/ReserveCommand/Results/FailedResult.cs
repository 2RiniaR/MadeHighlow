using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record FailedResult([NotNull] Command Command, FailedReason Code) : ReserveCommandResult
    {
        public override World Simulate(World world)
        {
            throw new InvalidOperationException("The failed event could not simulate.");
        }
    }
}
