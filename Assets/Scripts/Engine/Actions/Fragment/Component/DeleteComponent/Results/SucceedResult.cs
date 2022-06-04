using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public record SucceedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<RejectionContext>> RejectionInterrupts
    ) : Result;
}
