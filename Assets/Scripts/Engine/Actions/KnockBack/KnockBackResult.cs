using System;

namespace RineaR.MadeHighlow.Actions
{
    public record KnockBackResult() : Result(ActionType.KnockBack)
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}