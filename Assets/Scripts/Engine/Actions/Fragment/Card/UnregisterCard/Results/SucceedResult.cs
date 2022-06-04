using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterCard
{
    public record SucceedResult([NotNull] Action Action) : Result;
}
