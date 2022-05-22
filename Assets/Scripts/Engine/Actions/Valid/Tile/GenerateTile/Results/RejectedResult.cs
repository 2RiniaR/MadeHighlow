using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record RejectedResult(
        [NotNull] GenerateTileAction Action,
        [NotNull] GenerateTileProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateTileRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : GenerateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
