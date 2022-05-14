using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record ExemptedResult(
        [NotNull] Card Target,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PayCardEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
