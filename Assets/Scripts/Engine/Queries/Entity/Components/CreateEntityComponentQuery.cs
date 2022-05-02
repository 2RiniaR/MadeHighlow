using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record CreateEntityComponentQuery
    {
        [NotNull] public EntityLocator ParentLocator { get; init; } = new();
        [NotNull] public EntityComponent Value { get; init; } = EntityComponent.Empty;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            return new CreateMultiEntityComponentsQuery
            {
                ParentLocator = ParentLocator,
                Values = ValueObjectList.Create(Value),
            }.Run(world);
        }
    }
}