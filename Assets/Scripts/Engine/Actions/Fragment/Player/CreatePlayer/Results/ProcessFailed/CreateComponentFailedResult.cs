using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public record CreateComponentFailedResult(
        [NotNull] Action Action,
        [NotNull] Event<RegisterPlayer.Result> RegisterPlayerEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents,
        [NotNull] CreateComponent.Result Failed
    ) : Result;
}
