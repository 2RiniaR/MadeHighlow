using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record UpdateEntityQuery
    {
        [NotNull] public EntityLocator Locator { get; init; } = new();
        [NotNull] public Entity Value { get; init; } = Entity.Empty;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            return world with
            {
                Entities = world.Entities.ReplaceItem(entity => entity.ID == Locator.EntityID, Value),
            };
        }
    }
}