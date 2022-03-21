using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Components;

namespace RineaR.MadeHighlow.Engine.Queries.Objects.Components
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
                    Components = @object.Components.ReplaceItem(
                        component => component.ID == Locator.ComponentID,
                        Value
                    ),
                },
            }.Run(world);
        }
    }
}