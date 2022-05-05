using System;

namespace RineaR.MadeHighlow
{
    public abstract record SupplyCardResult(in SupplyCardResultCode Code) : ISimulatable
    {
        public World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}