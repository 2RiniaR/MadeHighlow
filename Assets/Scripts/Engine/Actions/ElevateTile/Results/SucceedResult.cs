using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public sealed record SucceedResult(
        ID SourceID,
        [NotNull] Tile Before,
        [NotNull] Elevate Elevate,
        [NotNull] [ItemNotNull] ValueList<Interrupt<ElevateTileEffect>> Interrupts,
        [NotNull] Tile After
    ) : ElevateTileResult
    {
        public override World Simulate(World world)
        {
            return After.UpdateIn(world);
        }
    }
}
