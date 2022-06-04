using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public record RejectionContext(
        [NotNull] IHistory History,
        [NotNull] ValueList<Interrupt> Collected,
        [NotNull] Action Action
    );
}
