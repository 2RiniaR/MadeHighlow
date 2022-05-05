using System;

namespace RineaR.MadeHighlow
{
    public record KnockBackResult : Result
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}