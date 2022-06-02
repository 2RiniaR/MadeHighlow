using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteComponent;

namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public record DeleteComponentFailedResult(
        [NotNull] DeleteCardAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] DeleteComponentResult Failed
    ) : DeleteCardResult;
}
