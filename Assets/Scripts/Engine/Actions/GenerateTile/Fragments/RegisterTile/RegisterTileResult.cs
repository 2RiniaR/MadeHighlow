using JetBrains.Annotations;
using RineaR.MadeHighlow.FragmentActions;

namespace RineaR.MadeHighlow.Actions.GenerateTile.RegisterTile
{
    public record RegisterTileResult([NotNull] AllocateIDResult AllocateIDResult, [NotNull] Tile Registered) : Result
    {
        public override World Simulate(World world)
        {
            world = AllocateIDResult.Simulate(world);
            world = Registered.CreateIn(world);
            return world;
        }
    }
}
