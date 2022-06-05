using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record Result([NotNull] Action Action) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public ValueList<Interrupt<CardReplacement>> Replacements { get; init; }
        public Event<ReactedResult<DropCard.Result>> DropCard { get; init; }
        public Event<CreateCard.Result> CreateCard { get; init; }
        public Rejection Rejection { get; init; }
    }
}
