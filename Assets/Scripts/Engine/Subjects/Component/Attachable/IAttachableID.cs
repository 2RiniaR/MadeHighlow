using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IAttachableID
    {
        public ID Content { get; init; }

        [NotNull] public static IAttachableID Empty => new EmptyAttachableEnsuredID();

        [CanBeNull]
        public IAttachable GetFrom([NotNull] in World world);

        private record EmptyAttachableEnsuredID : IAttachableID
        {
            public ID Content { get; init; } = ID.None;

            public IAttachable GetFrom(in World world)
            {
                return IAttachable.Empty;
            }
        }
    }
}