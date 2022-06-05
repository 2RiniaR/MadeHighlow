using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record Result([NotNull] Action Action) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public Event<PlaceCard.Result> PlaceCard { get; init; }
        public Rejection Rejection { get; init; }
        public Card Replaced { get; init; }
        public Card Supplied { get; init; }
    }
}
