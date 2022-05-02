using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record UpdateEntityComponentQuery
    {
        [NotNull] public EntityComponentLocator Locator { get; init; } = new();
        [NotNull] public EntityComponent Value { get; init; } = EntityComponent.Empty;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            var entity = new GetEntityQuery { Locator = Locator }.Run(world);
            return new UpdateEntityQuery
            {
                Locator = Locator,
                Value = entity with
                {
                    Components = entity.Components.ReplaceItem(component => component.ID == Locator.ComponentID, Value),
                },
            }.Run(world);
        }
    }
}