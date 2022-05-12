using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record RejectedResult(
        [NotNull] Tile InitialTile,
        [NotNull] ComponentID RejectedComponentID,
        [NotNull] SucceedProcess Process
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
