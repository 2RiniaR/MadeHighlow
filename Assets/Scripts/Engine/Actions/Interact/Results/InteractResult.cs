using System;

namespace RineaR.MadeHighlow
{
    public record InteractResult : Result
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}