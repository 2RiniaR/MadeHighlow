using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IAttachable : IObject
    {
        /// <summary>
        ///     ID
        /// </summary>
        [NotNull]
        public IAttachableID EnsuredID { get; }

        /// <summary>
        ///     コンポーネント
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public ValueObjectList<Component> Components { get; init; }

        [NotNull]
        public IAttachable WithComponents([NotNull] [ItemNotNull] ValueObjectList<Component> components);

        [NotNull]
        public World UpdateIn([NotNull] in World world);

        [NotNull] public static IAttachable Empty => new EmptyAttachable();

        private record EmptyAttachable : IAttachable
        {
            public ValueObjectList<Component> Components { get; init; } = ValueObjectList<Component>.Empty;

            public IAttachableID EnsuredID { get; } = IAttachableID.Empty;

            public IAttachable WithComponents(ValueObjectList<Component> components)
            {
                return Empty;
            }

            public World UpdateIn(in World world)
            {
                return world;
            }
        }
    }
}