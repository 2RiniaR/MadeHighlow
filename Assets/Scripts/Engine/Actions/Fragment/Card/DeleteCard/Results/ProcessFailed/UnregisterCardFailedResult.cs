using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public record UnregisterCardFailedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] UnregisterCard.Result Failed
    ) : Result;
}
