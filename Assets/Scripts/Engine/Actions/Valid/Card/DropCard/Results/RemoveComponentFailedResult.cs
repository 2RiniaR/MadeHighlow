using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public record RemoveComponentFailedResult(
        [NotNull] Card Target,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DropCardEffect>> Interrupts,
        [NotNull] ValueList<ReactedResult<RemoveComponent.SucceedResult>> SucceedResults,
        [NotNull] ReactedResult<RemoveComponentResult> FailedResult
    ) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
