using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record GenerateTileAction([NotNull] Tile InitialProps) : IValidAction;
}
