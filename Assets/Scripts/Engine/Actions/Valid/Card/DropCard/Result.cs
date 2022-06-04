using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.CheckRejection;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record Result([NotNull] Action Action) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public Event<DeleteCard.Result> DeleteCard { get; init; }
        public Rejection Rejection { get; init; }
    }
}
