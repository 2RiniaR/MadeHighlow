using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record NotFoundResult([NotNull] PayCardAction Action) : PayCardResult;
}
