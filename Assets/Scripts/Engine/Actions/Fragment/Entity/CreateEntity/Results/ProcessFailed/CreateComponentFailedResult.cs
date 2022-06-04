using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateEntity
{
    public record CreateComponentFailedResult(
        [NotNull] Action Action,
        [NotNull] Event<RegisterEntity.Result> RegisterEntityEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents,
        [NotNull] CreateComponent.Result Failed
    ) : Result;
}
