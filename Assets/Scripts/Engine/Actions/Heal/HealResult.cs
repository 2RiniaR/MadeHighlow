using System;

namespace RineaR.MadeHighlow.Actions
{
    public record HealResult() : Result(ActionType.Heal)
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}