using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateTile
{
    public interface IAction
    {
        [NotNull] Tile InitialProps { get; init; }
    }

    public record Action([NotNull] Tile InitialProps) : IAction;
}
