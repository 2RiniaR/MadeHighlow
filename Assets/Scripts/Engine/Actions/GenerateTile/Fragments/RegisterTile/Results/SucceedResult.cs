using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile.RegisterTile
{
    public record SucceedResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Tile Registered
    ) : RegisterTileResult
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = AllocateIDResult.Simulate(currentWorld);
            currentWorld = Registered.CreateIn(currentWorld);
            return currentWorld;
        }
    }
}
