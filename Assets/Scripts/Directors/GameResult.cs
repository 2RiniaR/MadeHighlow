using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Directors
{
    public record GameResult
    {
        [NotNull]
        [ItemNotNull]
        public ImmutableList<ID<Player>> Ranking { get; init; } = ImmutableList<ID<Player>>.Empty;
    }
}