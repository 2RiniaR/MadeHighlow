using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IAttachable : IObject
    {
        [NotNull] public IAttachableID AttachableID { get; }

        [NotNull] [ItemNotNull] public ValueList<Component> Components { get; init; }

        [NotNull]
        public IAttachable WithComponents([NotNull] [ItemNotNull] ValueList<Component> components);

        [NotNull]
        public World UpdateIn([NotNull] World world);
    }
}
