using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateTile
{
    public record CreateComponentFailedResult(
        [NotNull] Action Action,
        [NotNull] Event<RegisterTile.Result> RegisterTileEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents,
        [NotNull] CreateComponent.Result Failed
    ) : Result;
}
