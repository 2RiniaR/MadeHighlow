using System;
using RineaR.MadeHighlow.Engine.Events;

namespace RineaR.MadeHighlow.Engine.Actions.TurnIncrement
{
    public record TurnIncrementAction() : Action(ActionType.TurnIncrement)
    {
        public override EventTimeline Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}