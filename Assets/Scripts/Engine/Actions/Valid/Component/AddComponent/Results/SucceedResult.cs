using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
