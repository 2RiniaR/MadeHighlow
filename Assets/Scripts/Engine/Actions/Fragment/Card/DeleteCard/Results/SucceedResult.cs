using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public record SucceedResult
        ([NotNull] DeleteCardAction Action, [NotNull] DeleteCardProcess Process) : DeleteCardResult;
}
