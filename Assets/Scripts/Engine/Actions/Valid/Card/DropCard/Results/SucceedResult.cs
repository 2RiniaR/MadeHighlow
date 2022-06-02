using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record SucceedResult(
        [NotNull] DropCardAction Action,
        [NotNull] DropCardProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DropCardRejection>> RejectionInterrupts
    ) : DropCardResult;
}
