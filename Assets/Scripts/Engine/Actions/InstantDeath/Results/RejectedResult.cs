using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    /// <summary>
    ///     即死効果を防いだ結果
    /// </summary>
    public record RejectedResult(
        ID SourceID,
        [NotNull] EntityID TargetEntityID,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDeathEffect>> Interrupts,
        [NotNull] ComponentID RejectedComponentID
    ) : InstantDeathResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
