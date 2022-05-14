using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantHeal
{
    public record RejectedResult(
        ID SourceID,
        [NotNull] Entity Target,
        [NotNull] Heal Expected,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantHealEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : InstantHealResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
