using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public record TargetNotFoundResult([NotNull] Action Action) : Result;
}
