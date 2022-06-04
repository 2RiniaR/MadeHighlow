using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteTile
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
