using System;

namespace RineaR.MadeHighlow.Actions
{
    public record DamageResult() : Result(ActionType.Damage)
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}