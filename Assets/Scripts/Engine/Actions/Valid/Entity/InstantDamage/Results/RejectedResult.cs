using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public record RejectedResult(
        [NotNull] InstantDamageAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageEffect>> EffectInterrupts,
        [NotNull] Damage Calculated,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageRejection>> RejectInterrupts,
        [NotNull] ComponentID RejectedID
    ) : InstantDamageResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
