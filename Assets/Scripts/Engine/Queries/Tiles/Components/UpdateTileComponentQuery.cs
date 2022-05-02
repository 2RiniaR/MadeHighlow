using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record UpdateTileComponentQuery
    {
        [NotNull] public TileComponentLocator Locator { get; init; } = new();
        [NotNull] public TileComponent Value { get; init; } = TileComponent.Empty;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            var tile = new GetTileQuery { Locator = Locator }.Run(world);
            return new UpdateTileQuery
            {
                Locator = Locator,
                Value = tile with
                {
                    Components = tile.Components.ReplaceItem(component => component.ID == Locator.ComponentID, Value),
                },
            }.Run(world);
        }
    }
}