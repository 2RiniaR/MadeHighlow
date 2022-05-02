using System;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record RemoveComponentResult() : Result(ActionType.RemoveComponent)
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}