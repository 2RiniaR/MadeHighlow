using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Components;

namespace RineaR.MadeHighlow.Engine.Queries.Objects.Components
{
    public record GetAllComponentsQuery
    {
        [NotNull]
        public ImmutableList<Component> Run([NotNull] in World world)
        {
            return world.Objects.SelectMany(player => player.Components).ToImmutableList();
        }
    }
}