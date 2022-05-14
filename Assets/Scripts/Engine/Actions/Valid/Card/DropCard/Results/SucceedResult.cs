using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public record SucceedResult(
        [NotNull] Card Dropped,
        [NotNull] ValueList<ReactedResult<RemoveComponent.SucceedResult>> RemoveComponentResults,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DropCardEffect>> Interrupts
    ) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return Dropped.DeleteFrom(world);
        }
    }
}
