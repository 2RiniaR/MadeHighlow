using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public interface IDestroyEntityEffector
    {
        public ValueList<Interrupt<DestroyEntityEffect>> EffectsOnDestroyEntity(
            [NotNull] IHistory context,
            [NotNull] Entity target
        );
    }
}
