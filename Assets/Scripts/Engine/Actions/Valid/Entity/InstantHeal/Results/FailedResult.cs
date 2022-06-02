using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public record FailedResult([NotNull] InstantHealAction Action, FailedReason Reason) : InstantHealResult;
}
