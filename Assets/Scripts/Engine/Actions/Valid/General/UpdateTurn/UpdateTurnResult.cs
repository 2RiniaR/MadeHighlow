using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UpdateTurn
{
    public record UpdateTurnResult(
        [NotNull] UpdateTurnAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<IValidAction>> ActorInterrupts,
        [NotNull] UpdateTurnProcess Process
    ) : IValidResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new UpdateTurnSimulator(context, world, this).Simulate();
        }
    }
}
