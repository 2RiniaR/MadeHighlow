using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterTile
{
    public record NotFoundResult([NotNull] Action Action) : Result;
}
