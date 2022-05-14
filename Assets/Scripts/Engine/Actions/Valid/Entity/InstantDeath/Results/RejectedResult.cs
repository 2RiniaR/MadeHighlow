using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDeath
{
    public record RejectedResult(
        ID SourceID,
        [NotNull] Entity Target,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDeathEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : InstantDeathResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
