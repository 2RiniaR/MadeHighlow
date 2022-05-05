using System;

namespace RineaR.MadeHighlow
{
    public record DeathResult : Result
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}