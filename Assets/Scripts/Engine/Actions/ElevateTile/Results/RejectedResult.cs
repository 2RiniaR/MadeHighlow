using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public record RejectedResult(
        ID SourceID,
        [NotNull] Tile Target,
        [NotNull] [ItemNotNull] ValueList<Interrupt<ElevateTileEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : ElevateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
