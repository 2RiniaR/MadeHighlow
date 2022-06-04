using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
