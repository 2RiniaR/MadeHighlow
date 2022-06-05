using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection;

namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public record Result([NotNull] IAction Action) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public Event<PositionEntity.Result> PositionEntity { get; init; }
        public Rejection Rejection { get; init; }
        public Entity Moved { get; init; }
    }
}
