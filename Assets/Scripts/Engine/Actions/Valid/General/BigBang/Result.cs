using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.BigBang
{
    public record Result([NotNull] Action Action) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public ValueList<Event<ReactedResult<JoinPlayer.Result>>> JoinPlayers { get; init; }
        public ValueList<Event<ReactedResult<GenerateTile.Result>>> GenerateTiles { get; init; }
        public ValueList<Event<ReactedResult<GenerateEntity.Result>>> GenerateEntities { get; init; }
    }
}
