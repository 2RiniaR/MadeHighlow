using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「俊敏化」
    /// </summary>
    public record Haste(ID ID, [NotNull] IAttachableID AttachedID, [NotNull] Duration Duration) : Component(
        ID,
        AttachedID,
        Duration
    );
}