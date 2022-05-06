using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components
{
    public record BlockUnitPassing
        (in ID ID, [NotNull] in IAttachableID AttachedID, [NotNull] in Duration Duration) : Component(
            in ID,
            in AttachedID,
            in Duration
        );
}