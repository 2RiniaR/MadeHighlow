using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyTile
{
    public interface IDestroyTileRejector : IPriority<IDestroyTileRejector>
    {
        [NotNull]
        public Interrupt<DestroyTileRejection> DestroyTileRejection(
            [NotNull] IHistory history,
            [NotNull] DestroyTileAction action,
            [NotNull] DestroyTileProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyTileRejection>> collected
        );
    }
}
