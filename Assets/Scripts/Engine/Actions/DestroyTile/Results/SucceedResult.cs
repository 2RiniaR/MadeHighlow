using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public record SucceedResult(
        [NotNull] Tile Destroyed,
        [NotNull] ValueList<RemoveComponent.SucceedResult> RemoveComponentResults,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyTileEffect>> Interrupts
    ) : DestroyTileResult
    {
        public override World Simulate(World world)
        {
            world = RemoveComponentResults.Aggregate(world, (curr, result) => result.Simulate(curr));
            world = Destroyed.DeleteFrom(world);
            return world;
        }
    }
}
