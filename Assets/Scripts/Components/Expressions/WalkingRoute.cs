using System.Collections.Immutable;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.Expressions
{
    public record WalkingRoute([NotNull] ImmutableList<Direction2D> Steps);
}
