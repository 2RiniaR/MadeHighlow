using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public record SucceedResult(
        [NotNull] DeleteComponentAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DeleteComponentRejection>> RejectionInterrupts
    ) : DeleteComponentResult;
}
