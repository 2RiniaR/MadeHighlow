using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public record RejectedResult(
        [NotNull] MoveEntityAction Action,
        [NotNull] MoveEntityProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<MoveEntityRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : MoveEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
