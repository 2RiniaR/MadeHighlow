using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record DeleteCardFailedResult([NotNull] Action Action, [NotNull] DeleteCard.Result Failed) : Result;
}
