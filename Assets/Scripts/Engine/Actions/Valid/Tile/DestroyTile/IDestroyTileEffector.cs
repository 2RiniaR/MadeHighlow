using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyTile
{
    public interface IDestroyTileEffector
    {
        public ValueList<Interrupt<DestroyTileEffect>> EffectsOnDestroyTile(
            [NotNull] IHistory history,
            [NotNull] Tile target
        );
    }
}
