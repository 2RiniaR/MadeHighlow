using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IAttachable : IObject
    {
        /// <summary>
        ///     コンポーネント
        /// </summary>
        [NotNull]
        public ValueObjectList<Component> Components { get; init; }

        [NotNull] public static IAttachable Empty => new EmptyAttachable();

        [NotNull] public IAttachableEnsuredID EnsuredID { get; }

        [NotNull]
        public IAttachable WithComponents([NotNull] [ItemNotNull] ValueObjectList<Component> components);

        [NotNull]
        public World UpdateIn([NotNull] in World world);

        private record EmptyAttachable : IAttachable
        {
            public ValueObjectList<Component> Components { get; init; } = ValueObjectList<Component>.Empty;

            public IAttachableEnsuredID EnsuredID { get; } = IAttachableEnsuredID.Empty;

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