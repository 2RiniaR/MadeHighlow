using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record Process([NotNull] Event<PlaceCard.SucceedResult> PlaceCard)
    {
        public Timeline Timeline { get; } = new Timeline().Then(PlaceCard);
    }
}
