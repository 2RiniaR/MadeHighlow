using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record DisallowedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<Acceptance>> AcceptanceInterrupts,
        [CanBeNull] ComponentID DisallowedID
    ) : Result;
}
