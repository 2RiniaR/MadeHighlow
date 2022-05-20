using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityTeleport
{
    public interface IEntityTeleportRejector : IPriority<IEntityTeleportRejector>
    {
        [NotNull]
        public Interrupt<EntityTeleportRejection> EntityTeleportRejection(
            [NotNull] IHistory history,
            [NotNull] EntityTeleportAction action,
            [NotNull] EntityTeleportProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<EntityTeleportRejection>> collected
        );
    }
}
