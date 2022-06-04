using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteEntity
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
