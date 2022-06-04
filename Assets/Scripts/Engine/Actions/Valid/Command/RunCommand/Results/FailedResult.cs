using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record FailedResult([NotNull] Action Action, FailedReason Reason) : Result;
}
