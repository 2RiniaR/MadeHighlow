using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyEntity
{
    public interface IDestroyEntityEffector
    {
        public ValueList<Interrupt<DestroyEntityEffect>> EffectsOnDestroyEntity(
            [NotNull] IHistory history,
            [NotNull] Entity target
        );
    }
}
