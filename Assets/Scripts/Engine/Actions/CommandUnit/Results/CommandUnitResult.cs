using System;

namespace RineaR.MadeHighlow
{
    public abstract record CommandUnitResult(CommandUnitResultCode Code) : ISimulatable
    {
        public virtual World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}