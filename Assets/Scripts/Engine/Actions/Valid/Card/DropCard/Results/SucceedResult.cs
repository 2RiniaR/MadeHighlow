using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record SucceedResult(
        [NotNull] DropCardAction Action,
        [NotNull] DropCardProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DropCardRejection>> RejectionInterrupts
    ) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
