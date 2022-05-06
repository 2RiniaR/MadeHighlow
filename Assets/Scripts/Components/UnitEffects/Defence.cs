using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「防御」
    /// </summary>
    public record Defence(in ID ID, [NotNull] in IAttachableID AttachedID, [NotNull] in Duration Duration) : Component(
        in ID,
        in AttachedID,
        in Duration
    );
}