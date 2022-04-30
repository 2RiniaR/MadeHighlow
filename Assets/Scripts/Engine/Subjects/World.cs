using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「ゲーム全体の状態」を表現する
    /// </summary>
    public record World
    {
        [NotNull] public ValueObjectList<Player> Players { get; init; } = ValueObjectList<Player>.Empty;
        [NotNull] public ValueObjectList<Object> Objects { get; init; } = ValueObjectList<Object>.Empty;
        [NotNull] public Turn CurrentTurn { get; init; } = new();
    }
}