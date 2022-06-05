using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public interface IAction
    {
        [NotNull] Player InitialProps { get; init; }
    }

    public record Action([NotNull] Player InitialProps) : IAction;
}
