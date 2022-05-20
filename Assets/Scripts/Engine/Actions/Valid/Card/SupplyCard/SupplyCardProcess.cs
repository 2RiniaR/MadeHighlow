using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public record SupplyCardProcess([NotNull] Event<Fragment.PlaceCard.SucceedResult> PlaceCard)
    {
        public Timeline Timeline { get; } = new Timeline().Then(PlaceCard);
    }
}
