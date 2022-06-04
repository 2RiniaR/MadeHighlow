using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateEntity
{
    public record Result([NotNull] Action Action) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public Event<AllocateID.Result> AllocateID { get; init; }
        public Event<RegisterEntity.Result> RegisterEntity { get; init; }
        public ValueList<Event<CreateComponent.Result>> CreateComponents { get; init; }
        public Entity Created { get; init; }
    }
}
