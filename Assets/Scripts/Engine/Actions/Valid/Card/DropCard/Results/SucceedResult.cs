using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public record SucceedResult(
        [NotNull] DropCardAction Action,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DropCardEffect>> Interrupts
    ) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
