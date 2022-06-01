using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterCard
{
    public record UnregisterCardAction([NotNull] CardID TargetID);
}
