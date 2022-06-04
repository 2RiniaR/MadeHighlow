using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterCard
{
    public record NotFoundResult([NotNull] Action Action) : Result;
}
