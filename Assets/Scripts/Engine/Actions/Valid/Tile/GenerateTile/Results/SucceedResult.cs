using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record SucceedResult(
        [NotNull] Action Action,
        [NotNull] Process Process
    ) : Result;
}
