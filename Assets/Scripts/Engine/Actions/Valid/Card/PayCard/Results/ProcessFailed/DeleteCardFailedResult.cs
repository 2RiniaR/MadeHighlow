using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteCard;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record DeleteCardFailedResult(
        [NotNull] PayCardAction Action,
        [NotNull] DeleteCardResult Failed
    ) : PayCardResult;
}
