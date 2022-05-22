using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EntityTeleport;

namespace RineaR.MadeHighlow.Actions.ElevateTile
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
