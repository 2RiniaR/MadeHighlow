using System;

namespace RineaR.MadeHighlow.Actions.BigBang
{
    public record BigBangEvent : Event
    {
        protected BigBangEvent(BigBangResult result) : base(ActionType.BigBang)
        {
            Result = result;
        }

        public BigBangResult Result { get; }

        public static BigBangEvent FailedByNotEmpty => new(BigBangResult.FailedByNotEmpty);

        public override World Simulate(in World world)
        {
            throw new InvalidOperationException("The BigBang event could not simulate when session was not empty.");
        }
    }
}