using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public record RejectedResult(
        [NotNull] DestroyTileAction Action,
        [NotNull] DestroyTileProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyTileRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : DestroyTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
