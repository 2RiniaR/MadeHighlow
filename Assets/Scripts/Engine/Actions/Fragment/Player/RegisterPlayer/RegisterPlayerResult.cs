using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterPlayer
{
    public record RegisterPlayerResult([NotNull] RegisterPlayerAction Action, [NotNull] Player Registered) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new RegisterPlayerSimulator(context, world, this).Simulate();
        }
    }
}
