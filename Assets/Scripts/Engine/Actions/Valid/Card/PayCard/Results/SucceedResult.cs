using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public record SucceedResult(
        [NotNull] PayCardAction Action,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PayCardEffect>> Interrupts
    ) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
