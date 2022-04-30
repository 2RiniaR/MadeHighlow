using System;

namespace RineaR.MadeHighlow.Actions.Interact
{
    public record InteractEvent() : Event(ActionType.Interact)
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}