using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreateComponent;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateCard
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
