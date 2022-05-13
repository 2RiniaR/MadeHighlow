using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record RemoveComponentFailedResult(
        [NotNull] Card Target,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PayCardEffect>> Interrupts,
        [NotNull] ValueList<RemoveComponent.SucceedResult> SucceedResults,
        RemoveComponentResult FailedResult
    ) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
