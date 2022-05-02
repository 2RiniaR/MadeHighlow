using System;

namespace RineaR.MadeHighlow.Actions
{
    public record ActuateUnitResult() : Result(ActionType.ActuateUnit)
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}