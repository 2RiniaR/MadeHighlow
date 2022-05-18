using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.AddComponent;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public record AddComponentFailedResult(
        [NotNull] SupplyCardAction Action,
        [NotNull] Event<Fragment.RegisterCard.SucceedResult> RegisterCardEvent,
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<AddComponent.SucceedResult>>> AddComponentEvents,
        [NotNull] ReactedResult<AddComponentResult> Failed
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
