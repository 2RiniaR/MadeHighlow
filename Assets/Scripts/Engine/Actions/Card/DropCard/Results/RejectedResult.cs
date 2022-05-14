using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record RejectedResult(
        [NotNull] Card Target,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DropCardEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : DropCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
