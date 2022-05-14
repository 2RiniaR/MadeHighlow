using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
{
    public interface IGenerateTileEffector
    {
        public ValueList<Interrupt<GenerateTileEffect>> EffectsOnGenerateTile(
            [NotNull] IHistory history,
            [NotNull] Tile generation
        );
    }
}
