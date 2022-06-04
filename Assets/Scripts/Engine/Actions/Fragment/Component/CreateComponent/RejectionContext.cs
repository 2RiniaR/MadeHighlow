using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public record RejectionContext(
        [NotNull] IHistory History,
        [NotNull] ValueList<Interrupt> Collected,
        [NotNull] Action Action,
        [NotNull] Process Process
    );
}
