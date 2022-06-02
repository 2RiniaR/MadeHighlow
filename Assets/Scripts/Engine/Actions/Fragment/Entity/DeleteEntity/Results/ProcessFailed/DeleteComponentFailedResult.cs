using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteComponent;

namespace RineaR.MadeHighlow.Actions.DeleteEntity
{
    public record DeleteComponentFailedResult(
        [NotNull] DeleteEntityAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] DeleteComponentResult Failed
    ) : DeleteEntityResult;
}
