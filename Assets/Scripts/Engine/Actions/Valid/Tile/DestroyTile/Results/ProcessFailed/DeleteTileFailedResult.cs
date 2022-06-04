using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public record DeleteTileFailedResult([NotNull] Action Action, [NotNull] DeleteTile.Result Failed) : Result;
}
