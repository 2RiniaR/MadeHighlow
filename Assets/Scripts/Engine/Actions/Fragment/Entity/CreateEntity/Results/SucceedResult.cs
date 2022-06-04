using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateEntity
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
