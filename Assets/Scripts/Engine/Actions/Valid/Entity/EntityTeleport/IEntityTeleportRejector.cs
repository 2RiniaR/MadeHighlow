using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityTeleport
{
    public interface IEntityTeleportRejector : IPriority<IEntityTeleportRejector>
    {
        [CanBeNull]
        public Interrupt<EntityTeleportRejection> EntityTeleportRejection(
            [NotNull] IHistory history,
            [NotNull] EntityTeleportAction action,
            [NotNull] EntityTeleportProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<EntityTeleportRejection>> collected
        );
    }
}
