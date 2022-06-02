using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public record RejectedResult(
        [NotNull] DeleteComponentAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DeleteComponentRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : DeleteComponentResult;
}
