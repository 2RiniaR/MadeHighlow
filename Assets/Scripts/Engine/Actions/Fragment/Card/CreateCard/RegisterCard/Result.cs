using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateCard.RegisterCard
{
    public record Result([NotNull] Action Action) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public Card Registered { get; init; }
    }
}
