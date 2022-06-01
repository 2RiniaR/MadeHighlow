using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public static class ComponentGenerator
    {
        private sealed record FakeComponent(
            ID ID,
            [NotNull] IAttachableID AttachedID,
            [NotNull] Duration Duration
        ) : Component(ID, AttachedID, Duration);

        public static Component Empty => new FakeComponent(
            ID.None,
            AttachableIDGenerator.Fake(ID.None),
            new UnlimitedDuration()
        );
    }
}
