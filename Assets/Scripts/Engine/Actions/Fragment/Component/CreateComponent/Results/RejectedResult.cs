using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public record RejectedResult(
        [NotNull] CreateComponentAction Action,
        [NotNull] CreateComponentProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<CreateComponentRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : CreateComponentResult;
}
