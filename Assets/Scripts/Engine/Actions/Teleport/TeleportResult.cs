using System;

namespace RineaR.MadeHighlow.Actions
{
    public record TeleportResult() : Result(ActionType.Teleport)
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}