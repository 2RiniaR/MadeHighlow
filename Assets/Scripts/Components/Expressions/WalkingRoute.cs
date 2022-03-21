using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Geometry;

namespace RineaR.MadeHighlow.Components.Expressions
{
    public record WalkingRoute
    (
        [NotNull] ImmutableList<Direction2D> Steps
    );
}