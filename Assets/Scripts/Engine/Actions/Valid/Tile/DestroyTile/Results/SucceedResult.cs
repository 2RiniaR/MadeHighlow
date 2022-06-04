using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
