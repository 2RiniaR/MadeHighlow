using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterTile
{
    public record RegisterTileResult([NotNull] RegisterTileAction Action, [NotNull] Tile Registered) : Result
    {
        public override World Simulate(World world)
        {
            return Registered.CreateIn(world);
        }
    }
}
