using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record RejectedResult(
        ID SourceID,
        [NotNull] Entity Target,
        [NotNull] Damage Expected,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : InstantDamageResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
