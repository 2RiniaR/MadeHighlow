using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IGenerateEntityEffector
    {
        public ValueList<Interrupt<GenerateEntityEffect>> EffectsOnGenerateEntity(
            [NotNull] IActionContext context,
            [NotNull] GenerateEntityAction action
        );
    }
}
