using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public record RemoveComponentFailedResult(
        [NotNull] Card Target,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PayCardEffect>> Interrupts,
        [NotNull] ValueList<ReactedResult<RemoveComponent.SucceedResult>> SucceedResults,
        [NotNull] ReactedResult<RemoveComponentResult> FailedResult
    ) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
