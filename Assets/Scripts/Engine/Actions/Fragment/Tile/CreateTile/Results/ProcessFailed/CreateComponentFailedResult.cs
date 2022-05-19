using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreateComponent;
using RineaR.MadeHighlow.Actions.Fragment.RegisterTile;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateTile
{
    public record CreateComponentFailedResult(
        [NotNull] CreateTileAction Action,
        [NotNull] Event<RegisterTileResult> RegisterTileEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents,
        [NotNull] CreateComponentResult Failed
    ) : CreateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
