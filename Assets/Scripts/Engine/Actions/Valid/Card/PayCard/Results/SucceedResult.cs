using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record SucceedResult(
        [NotNull] PayCardAction Action,
        [NotNull] PayCardProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PayCardExemption>> ExemptionInterrupts
    ) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
