using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「掴まれ」
    /// </summary>
    public record Holden(in ID ID, [NotNull] in IAttachableID AttachedID, [NotNull] in Duration Duration) : Component(
        in ID,
        in AttachedID,
        in Duration
    );
}