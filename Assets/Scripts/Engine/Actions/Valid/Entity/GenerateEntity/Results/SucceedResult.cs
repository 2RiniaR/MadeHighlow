using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
