using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「毒」
    /// </summary>
    public record Poison(in ID ID, [NotNull] in IAttachableID AttachedID, [NotNull] in Duration Duration) : Component(
        in ID,
        in AttachedID,
        in Duration
    );
}