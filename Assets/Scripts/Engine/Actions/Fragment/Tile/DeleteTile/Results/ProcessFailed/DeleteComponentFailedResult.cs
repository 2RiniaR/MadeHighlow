using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteComponent;

namespace RineaR.MadeHighlow.Actions.DeleteTile
{
    public record DeleteComponentFailedResult(
        [NotNull] DeleteTileAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] DeleteComponentResult Failed
    ) : DeleteTileResult;
}
