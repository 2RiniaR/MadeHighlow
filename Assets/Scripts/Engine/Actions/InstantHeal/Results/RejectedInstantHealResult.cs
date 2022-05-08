using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒を防いだ結果
    /// </summary>
    public record RejectedInstantHealResult(
        ID SourceID,
        [NotNull] EntityID TargetEntityID,
        [NotNull] Heal ExpectedHeal,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantHealEffect>> Interrupts,
        [NotNull] ComponentID RejectedComponentID
    ) : InstantHealResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
