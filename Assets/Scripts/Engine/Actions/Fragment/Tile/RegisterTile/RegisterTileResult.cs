using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterTile
{
    public record RegisterTileResult([NotNull] RegisterTileAction Action, [NotNull] Tile Registered) : IResult
    {
        public World Simulate(SimulationContext context, World world)
        {
            return new RegisterTileSimulator(context, world, this).Simulate();
        }
    }
}
