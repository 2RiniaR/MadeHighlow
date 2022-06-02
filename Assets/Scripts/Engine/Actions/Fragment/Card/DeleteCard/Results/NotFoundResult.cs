using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public record NotFoundResult([NotNull] DeleteCardAction Action) : DeleteCardResult;
}
