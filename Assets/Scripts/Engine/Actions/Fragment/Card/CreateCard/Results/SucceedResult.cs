using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
