using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「攻撃力ブースト」
    /// </summary>
    public record StrengthBoost
        (in ID ID, [NotNull] in IAttachableID AttachedID, [NotNull] in Duration Duration) : Component(
            in ID,
            in AttachedID,
            in Duration
        );
}