using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public record Result([NotNull] IAction Action) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public Event<AllocateID.Result> AllocateID { get; init; }
        public Event<RegisterCard.Result> RegisterCard { get; init; }
        public ValueList<Event<CreateComponent.Result>> CreateComponents { get; init; }
        public Card Created { get; init; }
    }
}
