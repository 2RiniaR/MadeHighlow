using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityTeleport
{
    public interface IEntityTeleportEffector
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<EntityTeleportEffect>> EffectsOnTeleportEntity(
            [NotNull] IHistory history,
            [NotNull] Entity target,
            [NotNull] Position3D destination
        );
    }
}
