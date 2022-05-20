using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PlaceCard
{
    public interface IPlaceCardReplacer : IPriority<IPlaceCardReplacer>
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<CardReplacement>> CardReplacements(
            [NotNull] IHistory history,
            [NotNull] PlaceCardAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<CardReplacement>> collected
        );
    }
}
