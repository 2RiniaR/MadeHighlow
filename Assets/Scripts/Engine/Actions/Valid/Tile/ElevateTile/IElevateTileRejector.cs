using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.EntityTeleport;

namespace RineaR.MadeHighlow.Actions.Valid.ElevateTile
{
    public interface IElevateTileRejector : IPriority<IEntityTeleportRejector>
    {
        [CanBeNull]
        public Interrupt<ElevateTileRejection> ElevateTileRejection(
            [NotNull] IHistory history,
            [NotNull] ElevateTileAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<ElevateTileRejection>> collected
        );
    }
}
