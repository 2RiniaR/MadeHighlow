using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record NotFoundResult([NotNull] DropCardAction Action) : DropCardResult;
}
