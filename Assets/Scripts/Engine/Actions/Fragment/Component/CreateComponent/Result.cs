using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public record Result([NotNull] Action Action) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public Event<AllocateID.Result> AllocateID { get; init; }
        public Event<RegisterComponent.Result> RegisterComponent { get; init; }
        public Rejection Rejection { get; init; }
        public Component Created { get; init; }
    }
}
