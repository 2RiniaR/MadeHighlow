using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public interface IEntityFlyEffector
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<EntityFlyEffect>> EffectsOnFlyEntity(
            [NotNull] IHistory history,
            [NotNull] Entity target,
            [NotNull] Direction3D direction
        );
    }
}
