using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public interface IDestroyEntityEffector
    {
        public ValueList<Interrupt<DestroyEntityEffect>> EffectsOnGenerateEntity(
            [NotNull] IActionContext context,
            [NotNull] Entity target
        );
    }
}
