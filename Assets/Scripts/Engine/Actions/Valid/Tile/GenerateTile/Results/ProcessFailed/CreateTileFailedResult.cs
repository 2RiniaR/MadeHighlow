using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record CreateTileFailedResult([NotNull] Action Action, [NotNull] CreateTile.Result Failed) : Result;
}
