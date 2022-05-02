using System;

namespace RineaR.MadeHighlow.Actions
{
    public record BigBangResult : Result
    {
        protected BigBangResult(BigBangResultCode resultCode) : base(ActionType.BigBang)
        {
            ResultCode = resultCode;
        }

        public BigBangResultCode ResultCode { get; }

        public static BigBangResult FailedByNotEmpty => new(BigBangResultCode.FailedByNotEmpty);

        public override World Simulate(in World world)
        {
            throw new InvalidOperationException("The BigBang event could not simulate when session was not empty.");
        }
    }
}