using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateComponent
{
    public interface ICreateComponentEffector
    {
        public ValueList<Interrupt<CreateComponentEffect>> EffectsOnCreateComponent(
            [NotNull] IHistory history,
            [NotNull] CreateComponentAction action,
            [NotNull] Process process
        );
    }
}
