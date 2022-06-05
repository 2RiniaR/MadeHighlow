using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record Result([NotNull] Action Action) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public Event<DeleteEntity.Result> DeleteEntity { get; init; }
        public Rejection Rejection { get; init; }
        public bool Run { get; init; } = false;
    }
}
