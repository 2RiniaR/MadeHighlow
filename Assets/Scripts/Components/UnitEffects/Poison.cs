using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「毒」
    /// </summary>
    public record Poison(ID ID, [NotNull] IAttachableID AttachedID, [NotNull] Duration Duration) : Component(
        ID,
        AttachedID,
        Duration
    );
}