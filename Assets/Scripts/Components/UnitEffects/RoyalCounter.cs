using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「ロイヤルカウンター」
    /// </summary>
    public record RoyalCounter
        (in ID ID, [NotNull] in IAttachableID AttachedID, [NotNull] in Duration Duration) : Component(
            in ID,
            in AttachedID,
            in Duration
        );
}