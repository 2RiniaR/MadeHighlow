using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UpdateTurn
{
    public record Result(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<IValidAction>> ActorInterrupts,
        [NotNull] Process Process
    ) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }
    }
}
