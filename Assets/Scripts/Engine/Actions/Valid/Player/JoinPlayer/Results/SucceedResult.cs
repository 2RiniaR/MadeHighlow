using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
