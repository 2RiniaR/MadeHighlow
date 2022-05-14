using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public interface IElevateTileEffector
    {
        public ValueList<Interrupt<ElevateTileEffect>> EffectsOnElevateTile(
            [NotNull] IHistory history,
            ID sourceID,
            [NotNull] Tile target,
            [NotNull] Elevate elevate
        );
    }
}
