using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record SucceedResult(
        [NotNull] Card Dropped,
        [NotNull] ValueList<RemoveComponent.SucceedResult> RemoveComponentResults,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DropCardEffect>> Interrupts
    ) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return Dropped.DeleteFrom(world);
        }
    }
}
