using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PlaceCard
{
    public interface IPlaceCardRejector : IPriority<IPlaceCardRejector>
    {
        [CanBeNull]
        public Interrupt<PlaceCardRejection> PlaceCardRejection(
            [NotNull] IHistory history,
            [NotNull] PlaceCardAction action,
            [NotNull] PlaceCardProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<PlaceCardRejection>> collected
        );
    }
}
