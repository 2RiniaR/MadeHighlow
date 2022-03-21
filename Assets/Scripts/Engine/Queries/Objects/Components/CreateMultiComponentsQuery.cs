using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Components;

namespace RineaR.MadeHighlow.Engine.Queries.Objects.Components
{
    public record CreateMultiComponentsQuery
    {
        [NotNull] public ObjectLocator ParentLocator { get; init; } = new();
        [NotNull] [ItemNotNull] public ImmutableList<Component> Values { get; init; } = ImmutableList<Component>.Empty;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            var player = new GetObjectQuery { Locator = ParentLocator }.Run(world);
            return new UpdateObjectQuery
            {
                Locator = ParentLocator,
                Value = player with { Components = player.Components.AddRange(Values) },
            }.Run(world);
        }
    }
}