using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IRegisterEntityEffector
    {
        public ValueList<Interrupt<RegisterEntityEffect>> EffectsOnRegisterEntity(
            [NotNull] IActionContext context,
            [NotNull] RegisterEntityAction action
        );
    }
}
