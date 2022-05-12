using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record SucceedResult(
        [NotNull] Tile InitialTile,
        [NotNull] Tile GeneratedTile,
        [NotNull] SucceedProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateTileEffect>> Interrupts
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = Process.RegisterTile.Simulate(currentWorld);
            currentWorld = Process.AddComponents.Aggregate(currentWorld, (curr, result) => result.Simulate(curr));
            currentWorld = Process.PositionTile.Simulate(currentWorld);
            return currentWorld;
        }
    }
}
