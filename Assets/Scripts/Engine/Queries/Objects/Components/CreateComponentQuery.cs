using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Components
{
    public record CreateComponentQuery
    {
        [NotNull] public ObjectLocator ParentLocator { get; init; } = new();
        [CanBeNull] public Component Value { get; init; } = null;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            if (Value == null) return world;
            return new CreateMultiComponentsQuery
            {
                ParentLocator = ParentLocator,
                Values = ValueObjectList.Create(Value),
            }.Run(world);
        }
    }
}