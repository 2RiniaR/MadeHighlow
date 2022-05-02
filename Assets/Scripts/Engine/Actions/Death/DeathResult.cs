using System;

namespace RineaR.MadeHighlow.Actions
{
    public record DeathResult() : Result(ActionType.Death)
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}