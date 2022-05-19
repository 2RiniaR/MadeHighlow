using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreateComponent;
using RineaR.MadeHighlow.Actions.Fragment.RegisterPlayer;

namespace RineaR.MadeHighlow.Actions.Fragment.CreatePlayer
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
