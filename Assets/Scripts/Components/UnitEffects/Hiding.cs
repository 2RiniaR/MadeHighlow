using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「ドロン」
    /// </summary>
    public record Hiding(ID ID, [NotNull] IAttachableID AttachedID, [NotNull] Duration Duration) : Component(
        ID,
        AttachedID,
        Duration
    );
}