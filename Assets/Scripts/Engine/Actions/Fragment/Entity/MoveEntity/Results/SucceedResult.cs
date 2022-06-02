using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public record SucceedResult(
        [NotNull] MoveEntityAction Action,
        [NotNull] MoveEntityProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<MoveEntityRejection>> RejectionInterrupts
    ) : MoveEntityResult;
}
