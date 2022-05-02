using System;

namespace RineaR.MadeHighlow.Actions
{
    public record FailedCommandUnitResult : CommandUnitResult
    {
        private FailedCommandUnitResult(CommandUnitResultCode code) : base(code)
        {
        }

        public static FailedCommandUnitResult NotOwner => new(CommandUnitResultCode.NotOwner);
        public static FailedCommandUnitResult TargetNotFound => new(CommandUnitResultCode.TargetNotFound);
        public static FailedCommandUnitResult InvalidOption => new(CommandUnitResultCode.InvalidOption);

        public override World Simulate(in World world)
        {
            throw new InvalidOperationException("The failed event could not simulate.");
        }
    }
}