using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Components
{
    public record CreateMultiComponentsQuery
    {
        [NotNull] public ObjectLocator ParentLocator { get; init; } = new();

        [NotNull]
        [ItemNotNull]
        public ValueObjectList<Component> Values { get; init; } = ValueObjectList<Component>.Empty;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            var player = new GetObjectQuery { Locator = ParentLocator }.Run(world);
            return new UpdateObjectQuery
            {
                Locator = ParentLocator,
                Value = player with { Components = player.Components.Items.AddRange(Values.Items).ToValueObjectList() },
            }.Run(world);
        }
    }
}