using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public record IncrementTurnResult([NotNull] Turn Updated) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new IncrementTurnSimulator(context, world, this).Simulate();
        }
    }
}
