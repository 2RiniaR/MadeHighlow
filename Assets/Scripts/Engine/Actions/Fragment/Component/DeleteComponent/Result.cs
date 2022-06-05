using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection;

namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public record Result([NotNull] IAction Action) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public Rejection Rejection { get; init; }
        public ComponentID Deleted { get; init; }
    }
}
