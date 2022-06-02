using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public record SucceedResult(
        [NotNull] CreateComponentAction Action,
        [NotNull] CreateComponentProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<CreateComponentRejection>> RejectionInterrupts
    ) : CreateComponentResult;
}
