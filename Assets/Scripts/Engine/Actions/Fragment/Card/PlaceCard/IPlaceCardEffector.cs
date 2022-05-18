using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PlaceCard
{
    public interface IPlaceCardEffector : IPriority<IPlaceCardEffector>
    {
        public ValueList<Interrupt<PlaceCardEffect>> EffectsOnPlaceCard(
            [NotNull] IHistory history,
            [NotNull] PlaceCardAction action,
            [NotNull] Process process
        );
    }
}
