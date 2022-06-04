using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public record FailedResult([NotNull] Action Action, FailedReason Reason) : Result;
}
