using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PlaceCard
{
    public interface IPlaceCardReplaceEffector : IPriority<IPlaceCardReplaceEffector>
    {
        public ValueList<Interrupt<PlaceCardReplaceEffect>> ReplaceEffectsOnPutCard(
            [NotNull] IHistory history,
            [NotNull] PlaceCardAction action
        );
    }
}
