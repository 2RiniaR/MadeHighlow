using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public interface IDestroyTileEffector
    {
        public ValueList<Interrupt<DestroyTileEffect>> EffectsOnDestroyTile(
            [NotNull] IActionContext context,
            [NotNull] Tile target
        );
    }
}
