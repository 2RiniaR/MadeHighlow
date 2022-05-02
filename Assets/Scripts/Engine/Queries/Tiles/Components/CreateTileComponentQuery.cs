using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record CreateTileComponentQuery
    {
        [NotNull] public TileLocator ParentLocator { get; init; } = new();
        [NotNull] public TileComponent Value { get; init; } = TileComponent.Empty;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            return new CreateMultiTileComponentsQuery
            {
                ParentLocator = ParentLocator,
                Values = ValueObjectList.Create(Value),
            }.Run(world);
        }
    }
}