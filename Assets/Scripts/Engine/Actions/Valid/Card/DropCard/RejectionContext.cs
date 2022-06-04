using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record RejectionContext(
        [NotNull] IHistory History,
        [NotNull] Result Result,
        [NotNull] ValueList<Interrupt> Collected
    );
}
