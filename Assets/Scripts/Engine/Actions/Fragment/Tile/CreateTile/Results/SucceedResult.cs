using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateTile
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
