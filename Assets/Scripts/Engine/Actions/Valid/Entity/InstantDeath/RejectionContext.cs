using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public record RejectionContext(
        [NotNull] IHistory History,
        [NotNull] Result Result,
        [NotNull] ValueList<Interrupt> Collected
    );
}
