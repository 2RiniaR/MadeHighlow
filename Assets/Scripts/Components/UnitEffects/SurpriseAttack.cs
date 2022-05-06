using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「不意打ち」
    /// </summary>
    public record SurpriseAttack
        (in ID ID, [NotNull] in IAttachableID AttachedID, [NotNull] in Duration Duration) : Component(
            in ID,
            in AttachedID,
            in Duration
        );
}