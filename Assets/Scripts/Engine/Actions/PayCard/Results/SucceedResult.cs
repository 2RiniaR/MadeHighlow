using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record SucceedResult(
        [NotNull] Card Paid,
        [NotNull] ValueList<RemoveComponent.SucceedResult> RemoveComponentResults,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PayCardEffect>> Interrupts
    ) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return Paid.DeleteFrom(world);
        }
    }
}
