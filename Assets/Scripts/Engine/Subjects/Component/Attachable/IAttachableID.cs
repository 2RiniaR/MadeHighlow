using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IAttachableID
    {
        public ID Content { get; init; }

        [CanBeNull]
        public IAttachable GetFrom([NotNull] World world);
    }
}
