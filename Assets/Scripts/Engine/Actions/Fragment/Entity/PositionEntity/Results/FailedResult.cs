using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionEntity
{
    public record FailedResult([NotNull] PositionEntityAction Action, FailedReason Reason) : PositionEntityResult;
}
