using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateComponent;
using RineaR.MadeHighlow.Actions.RegisterTile;

namespace RineaR.MadeHighlow.Actions.CreateTile
{
    public record CreateComponentFailedResult(
        [NotNull] CreateTileAction Action,
        [NotNull] Event<RegisterTileResult> RegisterTileEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents,
        [NotNull] CreateComponentResult Failed
    ) : CreateTileResult;
}
