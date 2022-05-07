using System.Collections.Immutable;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Directors
{
    public record GameResult
    {
        [NotNull] public ImmutableList<ID> Ranking { get; init; } = ImmutableList<ID>.Empty;
    }
}
