using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public interface IGenerateTileEffector
    {
        public ValueList<Interrupt<GenerateTileEffect>> EffectsOnGenerateTile(
            [NotNull] IActionContext context,
            [NotNull] GenerateTileAction action
        );
    }
}
