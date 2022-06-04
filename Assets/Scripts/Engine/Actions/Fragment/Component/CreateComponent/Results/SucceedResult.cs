using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
