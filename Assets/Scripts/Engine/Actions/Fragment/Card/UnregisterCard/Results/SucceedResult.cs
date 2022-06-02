using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterCard
{
    public record SucceedResult([NotNull] UnregisterCardAction Action) : UnregisterCardResult;
}
