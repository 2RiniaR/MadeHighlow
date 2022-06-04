using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record DestinationNotFoundResult([NotNull] Action Action) : Result;
}
