using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record CreateMultiEntityComponentsQuery
    {
        [NotNull] public EntityLocator ParentLocator { get; init; } = new();

        [NotNull]
        [ItemNotNull]
        public ValueObjectList<EntityComponent> Values { get; init; } = ValueObjectList<EntityComponent>.Empty;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            var player = new GetEntityQuery { Locator = ParentLocator }.Run(world);
            return new UpdateEntityQuery
            {
                Locator = ParentLocator,
                Value = player with { Components = player.Components.AddRange(Values) },
            }.Run(world);
        }
    }
}