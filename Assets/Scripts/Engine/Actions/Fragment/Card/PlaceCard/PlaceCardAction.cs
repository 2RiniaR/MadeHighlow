using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record PlaceCardAction([NotNull] PlayerID ParentID, [NotNull] Card InitialProps);
}
