using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateComponent;

namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public record CreateComponentFailedResult(
        [NotNull] CreateCardAction Action,
        [NotNull] Event<RegisterCard.SucceedResult> RegisterCardEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents,
        [NotNull] CreateComponentResult Failed
    ) : CreateCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
