using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public interface IDestroyTileRejector : IPriority<IDestroyTileRejector>
    {
        [CanBeNull]
        public Interrupt<DestroyTileRejection> DestroyTileRejection(
            [NotNull] IHistory history,
            [NotNull] DestroyTileAction action,
            [NotNull] DestroyTileProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyTileRejection>> collected
        );
    }
}
