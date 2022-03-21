using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Components;

namespace RineaR.MadeHighlow.Engine.Queries.Objects.Components
{
    public record CreateComponentQuery
    {
        [NotNull] public ObjectLocator ParentLocator { get; init; } = new();
        [CanBeNull] public Component Value { get; init; } = null;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            if (Value == null) return world;
            return new CreateMultiComponentsQuery
            {
                ParentLocator = ParentLocator,
                Values = ImmutableList.Create(Value),
            }.Run(world);
        }
    }
}