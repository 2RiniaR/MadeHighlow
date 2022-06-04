using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public record FailedResult([NotNull] Action Action, FailedReason Reason) : Result;
}
