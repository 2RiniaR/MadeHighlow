using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public record Result([NotNull] Action Action) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public ValueList<Event<DeleteComponent.Result>> DeleteComponents { get; init; }
        public Event<UnregisterCard.Result> UnregisterCard { get; init; }
        public CardID DeletedID { get; init; }
    }
}
