using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public record Action([NotNull] PlayerID ParentID, [NotNull] Card InitialProps);
}
