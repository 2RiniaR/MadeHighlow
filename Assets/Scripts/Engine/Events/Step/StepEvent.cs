using System;
using RineaR.MadeHighlow.Engine.Subjects;

namespace RineaR.MadeHighlow.Engine.Events.Step
{
    public record StepEvent() : Event(EventType.Step)
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}