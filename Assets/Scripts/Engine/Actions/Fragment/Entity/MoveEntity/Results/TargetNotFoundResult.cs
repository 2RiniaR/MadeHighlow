using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public record TargetNotFoundResult([NotNull] MoveEntityAction Action) : MoveEntityResult;
}
