using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record ParentNotFoundResult([NotNull] PlaceCardAction Action) : PlaceCardResult;
}
