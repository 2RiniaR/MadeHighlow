using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public interface IDestroyTileEffector
    {
        public ValueList<Interrupt<DestroyTileEffect>> EffectsOnDestroyTile(
            [NotNull] IHistory context,
            [NotNull] Tile target
        );
    }
}
