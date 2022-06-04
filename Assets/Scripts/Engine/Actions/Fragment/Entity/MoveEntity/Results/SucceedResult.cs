using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
