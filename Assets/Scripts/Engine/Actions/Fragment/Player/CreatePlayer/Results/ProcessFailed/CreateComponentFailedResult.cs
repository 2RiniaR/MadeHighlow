using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateComponent;
using RineaR.MadeHighlow.Actions.RegisterPlayer;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public record CreateComponentFailedResult(
        [NotNull] CreatePlayerAction Action,
        [NotNull] Event<RegisterPlayerResult> RegisterPlayerEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents,
        [NotNull] CreateComponentResult Failed
    ) : CreatePlayerResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
