using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public interface IAction
    {
        [NotNull] PlayerID ParentID { get; init; }
        [NotNull] Card InitialProps { get; init; }
    }

    public record Action([NotNull] PlayerID ParentID, [NotNull] Card InitialProps) : IAction;
}
