using System.Collections.Immutable;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Directors
{
    public record GameResult
    {
        [NotNull]
        [ItemNotNull]
        public ImmutableList<ID<Player>> Ranking { get; init; } = ImmutableList<ID<Player>>.Empty;
    }
}