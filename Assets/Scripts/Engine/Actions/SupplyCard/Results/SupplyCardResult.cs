using System;

namespace RineaR.MadeHighlow
{
    public abstract record SupplyCardResult(in SupplyCardResultCode Code) : Result
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}