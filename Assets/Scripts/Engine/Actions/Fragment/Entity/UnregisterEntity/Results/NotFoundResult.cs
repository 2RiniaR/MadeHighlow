using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterEntity
{
    public record NotFoundResult([NotNull] Action Action) : Result;
}
