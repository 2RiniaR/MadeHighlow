using System;

namespace RineaR.MadeHighlow.Actions
{
    public record FailedStepResult : StepResult
    {
        private FailedStepResult(StepResultCode code) : base(code)
        {
        }

        public static FailedStepResult InvalidActor => new(StepResultCode.InvalidActor);
        public static FailedStepResult NoEntry => new(StepResultCode.NoEntry);
        public static FailedStepResult CostOver => new(StepResultCode.CostOver);

        public override World Simulate(in World world)
        {
            throw new InvalidOperationException("The failed event could not simulate.");
        }
    }
}