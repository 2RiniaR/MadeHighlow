using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public record CreateCardAction([NotNull] PlayerID ParentID, [NotNull] Card InitialProps);
}
