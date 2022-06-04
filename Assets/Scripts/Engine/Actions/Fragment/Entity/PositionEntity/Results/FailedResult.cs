using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionEntity
{
    public record FailedResult([NotNull] Action Action, FailedReason Reason) : Result;
}
