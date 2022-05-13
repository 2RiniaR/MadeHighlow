using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public interface IGenerateEntityEffector
    {
        public ValueList<Interrupt<GenerateEntityEffect>> EffectsOnGenerateEntity(
            [NotNull] IActionContext context,
            [NotNull] Entity generation
        );
    }
}
