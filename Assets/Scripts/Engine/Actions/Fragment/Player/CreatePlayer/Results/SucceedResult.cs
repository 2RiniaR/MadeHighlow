using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
