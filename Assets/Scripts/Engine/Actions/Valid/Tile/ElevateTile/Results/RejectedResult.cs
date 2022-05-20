using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.ElevateTile
{
    public record RejectedResult(
        [NotNull] ElevateTileAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<ElevateTileRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : ElevateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
