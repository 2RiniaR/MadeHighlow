using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public interface IAddComponentEffector
    {
        public ValueList<Interrupt<AddComponentEffect>> EffectsOnAddComponent(
            [NotNull] IHistory context,
            [NotNull] Component generation
        );
    }
}
