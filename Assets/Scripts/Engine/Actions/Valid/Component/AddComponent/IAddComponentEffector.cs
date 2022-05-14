using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.AddComponent
{
    public interface IAddComponentEffector
    {
        public ValueList<Interrupt<AddComponentEffect>> EffectsOnAddComponent(
            [NotNull] IHistory history,
            [NotNull] Component generation
        );
    }
}
