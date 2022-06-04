using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public record PositionFailedResult([NotNull] Action Action, [NotNull] PositionEntity.Result Failed) : Result;
}
