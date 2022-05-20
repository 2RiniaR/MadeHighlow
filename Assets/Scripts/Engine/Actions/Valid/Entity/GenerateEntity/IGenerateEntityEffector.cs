using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateEntity
{
    public interface IGenerateEntityEffector
    {
        public ValueList<Interrupt<GenerateEntityEffect>> EffectsOnGenerateEntity(
            [NotNull] IHistory history,
            [NotNull] GenerateEntityAction action,
            [NotNull] GenerateEntityProcess process
        );
    }
}
