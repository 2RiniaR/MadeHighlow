using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record Result([NotNull] Action Action) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public ValueList<Event<ReactedResult<IValidResult>>> CommandActions { get; init; }
        public Event<ReactedResult<PayCard.Result>> PayCard { get; init; }
        public Rejection Rejection { get; init; }
        public bool Run { get; init; }
    }
}
