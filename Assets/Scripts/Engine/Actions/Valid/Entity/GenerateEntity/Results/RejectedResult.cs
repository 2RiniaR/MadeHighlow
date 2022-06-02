using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record RejectedResult(
        [NotNull] GenerateEntityAction Action,
        [NotNull] GenerateEntityProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateEntityRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : GenerateEntityResult;
}
