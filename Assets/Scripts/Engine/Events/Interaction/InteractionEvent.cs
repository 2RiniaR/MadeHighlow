using System;
using RineaR.MadeHighlow.Engine.Subjects;

namespace RineaR.MadeHighlow.Engine.Events.Interaction
{
    public record InteractionEvent() : Event(EventType.Interaction)
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}