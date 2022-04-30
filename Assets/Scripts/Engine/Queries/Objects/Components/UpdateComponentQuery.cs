using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Components
{
    public record UpdateComponentQuery
    {
        [NotNull] public ComponentLocator Locator { get; init; } = new();
        [CanBeNull] public Component Value { get; init; } = null;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            if (Value == null) return world;
            var @object = new GetObjectQuery { Locator = Locator }.Run(world);
            return new UpdateObjectQuery
            {
                Locator = Locator,
                Value = @object with
                {
                    Components = @object.Components.Items.ReplaceItem(
                            component => component.ID == Locator.ComponentID,
                            Value
                        )
                        .ToValueObjectList(),
                },
            }.Run(world);
        }
    }
}