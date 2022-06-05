using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record Acceptance([NotNull] ComponentID Applied, bool IsAllowed);
}
