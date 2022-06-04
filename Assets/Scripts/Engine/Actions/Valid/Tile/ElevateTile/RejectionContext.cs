using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public record RejectionContext(
        [NotNull] IHistory History,
        [NotNull] ValueList<Interrupt> Collected,
        [NotNull] Action Action
    );
}
