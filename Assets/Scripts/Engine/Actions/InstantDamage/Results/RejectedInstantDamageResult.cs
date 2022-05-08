using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを防いだ結果
    /// </summary>
    public record RejectedInstantDamageResult(
        ID SourceID,
        [NotNull] EntityID TargetEntityID,
        [NotNull] Damage ExpectedDamage,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageEffect>> Interrupts,
        [NotNull] ComponentID RejectedComponentID
    ) : InstantDamageResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
