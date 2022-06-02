using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.UnregisterCard;

namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public record UnregisterCardFailedResult(
        [NotNull] DeleteCardAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] UnregisterCardResult Failed
    ) : DeleteCardResult;
}
