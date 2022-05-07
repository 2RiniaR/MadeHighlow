using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.UnitEffects
{
    /// <summary>
    ///     「掴まれ」
    /// </summary>
    public record Holden(ID ID, [NotNull] IAttachableID AttachedID, [NotNull] Duration Duration) : Component(
        ID,
        AttachedID,
        Duration
    );
}