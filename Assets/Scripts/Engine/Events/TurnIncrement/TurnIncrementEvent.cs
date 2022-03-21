using RineaR.MadeHighlow.Engine.Subjects;

namespace RineaR.MadeHighlow.Engine.Events.TurnIncrement
{
    public record TurnIncrementEvent() : Event(EventType.TurnIncrement)
    {
        public override World Simulate(in World world)
        {
            return world with { CurrentTurn = new Turn(world.CurrentTurn.Value + 1) };
        }
    }
}