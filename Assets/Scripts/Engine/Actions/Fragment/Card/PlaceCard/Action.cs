using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record Action([NotNull] PlayerID ParentID, [NotNull] Card InitialProps);
}
