using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record ReplacementContext(
        [NotNull] IHistory History,
        [NotNull] Result Result,
        [NotNull] ValueList<Interrupt<Replacement>> Collected
    );
}
