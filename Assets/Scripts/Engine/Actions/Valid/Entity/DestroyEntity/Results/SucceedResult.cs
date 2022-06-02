using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record SucceedResult(
        [NotNull] DestroyEntityAction Action,
        [NotNull] DestroyEntityProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyEntityRejection>> RejectionInterrupts
    ) : DestroyEntityResult;
}
