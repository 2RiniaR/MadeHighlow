using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「エナジーバリア」
    /// </summary>
    public record EnergyBarrier
        (in ID ID, [NotNull] in IAttachableID AttachedID, [NotNull] in Duration Duration) : Component(
            in ID,
            in AttachedID,
            in Duration
        );
}