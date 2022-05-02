using System;

namespace RineaR.MadeHighlow.Actions
{
    public record InteractResult() : Result(ActionType.Interact)
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}