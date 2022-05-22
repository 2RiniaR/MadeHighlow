using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public interface IPlaceCardReplacer : IPriority<IPlaceCardReplacer>
    {
        [ItemNotNull]
        [CanBeNull]
        public ValueList<Interrupt<CardReplacement>> CardReplacements(
            [NotNull] IHistory history,
            [NotNull] PlaceCardAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<CardReplacement>> collected
        );
    }
}
