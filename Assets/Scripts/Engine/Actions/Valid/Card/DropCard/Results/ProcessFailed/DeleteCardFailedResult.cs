using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record DeleteCardFailedResult([NotNull] Action Action, [NotNull] DeleteCard.Result Failed) : Result;
}
