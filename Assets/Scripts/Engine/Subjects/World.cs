using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Engine.Subjects
{
    /// <summary>
    ///     「ゲーム全体の状態」を表現する
    /// </summary>
    public record World
    {
        [NotNull] public ImmutableList<Player> Players { get; init; } = ImmutableList<Player>.Empty;
        [NotNull] public ImmutableList<Object> Objects { get; init; } = ImmutableList<Object>.Empty;
        [NotNull] public Turn CurrentTurn { get; init; } = new();
    }
}