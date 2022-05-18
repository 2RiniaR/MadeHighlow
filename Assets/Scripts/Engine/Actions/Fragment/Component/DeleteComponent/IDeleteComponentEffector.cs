using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteComponent
{
    public interface IDeleteComponentEffector
    {
        public ValueList<Interrupt<DeleteComponentEffect>> EffectsOnDeleteComponent(
            [NotNull] IHistory history,
            [NotNull] DeleteComponentAction action
        );
    }
}
