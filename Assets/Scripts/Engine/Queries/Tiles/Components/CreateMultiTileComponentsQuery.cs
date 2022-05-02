using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record CreateMultiTileComponentsQuery
    {
        [NotNull] public TileLocator ParentLocator { get; init; } = new();

        [NotNull]
        [ItemNotNull]
        public ValueObjectList<TileComponent> Values { get; init; } = ValueObjectList<TileComponent>.Empty;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            var player = new GetTileQuery { Locator = ParentLocator }.Run(world);
            return new UpdateTileQuery
            {
                Locator = ParentLocator,
                Value = player with { Components = player.Components.AddRange(Values) },
            }.Run(world);
        }
    }
}