using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
{
    public interface IGenerateTileRejector : IPriority<IGenerateTileRejector>
    {
        [NotNull]
        public Interrupt<GenerateTileRejection> GenerateTileRejection(
            [NotNull] IHistory history,
            [NotNull] GenerateTileAction action,
            [NotNull] GenerateTileProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateTileRejection>> collected
        );
    }
}
