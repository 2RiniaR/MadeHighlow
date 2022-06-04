using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public record FailedResult([NotNull] Action Action, FailedReason Reason) : Result;
}
