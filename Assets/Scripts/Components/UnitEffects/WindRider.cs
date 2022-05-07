using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「風乗り」
    /// </summary>
    public record WindRider
        (ID ID, [NotNull] IAttachableID AttachedID, [NotNull] Duration Duration) : Component(
            ID,
            AttachedID,
            Duration
        );
}