using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PutCard;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public record PutCardFailedResult(
        [NotNull] SupplyCardAction Action,
        [NotNull] Event<Fragment.RegisterCard.SucceedResult> RegisterCardEvent,
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<AddComponent.SucceedResult>>> AddComponentEvents,
        [NotNull] PutCardResult Failed
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
