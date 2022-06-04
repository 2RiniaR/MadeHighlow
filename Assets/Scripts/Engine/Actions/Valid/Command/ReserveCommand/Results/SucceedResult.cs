using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record SucceedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<Acceptance>> AcceptanceInterrupts,
        [NotNull] ComponentID AllowedID
    ) : Result;
}
