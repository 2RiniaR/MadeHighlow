using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「エナジーバリア」
    /// </summary>
    public record EnergyBarrier
        (ID ID, [NotNull] IAttachableID AttachedID, [NotNull] Duration Duration) : Component(
            ID,
            AttachedID,
            Duration
        );
}