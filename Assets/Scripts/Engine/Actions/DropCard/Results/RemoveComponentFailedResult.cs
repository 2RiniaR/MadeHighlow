using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record RemoveComponentFailedResult(
        [NotNull] Card Target,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DropCardEffect>> Interrupts,
        [NotNull] ValueList<RemoveComponent.SucceedResult> SucceedResults,
        RemoveComponentResult FailedResult
    ) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
