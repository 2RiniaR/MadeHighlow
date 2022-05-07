using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components
{
    public record BlockUnitPassing(ID ID, [NotNull] IAttachableID AttachedID, [NotNull] Duration Duration) : Component(
        ID,
        AttachedID,
        Duration
    );
}
