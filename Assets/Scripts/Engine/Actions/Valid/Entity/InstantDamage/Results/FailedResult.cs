using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record FailedResult([NotNull] Action Action, FailedReason Reason) : Result;
}
