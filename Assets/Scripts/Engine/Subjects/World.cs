using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ゲーム全体の状態
    /// </summary>
    public record World(
        ID LatestAllocatedID,
        [NotNull] [ItemNotNull] ValueList<Player> Players,
        [NotNull] [ItemNotNull] ValueList<Tile> Tiles,
        [NotNull] [ItemNotNull] ValueList<Entity> Entities,
        [NotNull] Turn CurrentTurn,
        [NotNull] [ItemNotNull] ValueList<Command> ReservedCommands
    )
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<IObject> GetChildren()
        {
            return ValueList.Concat(
                Players.Select(item => item as IObject),
                Players.SelectMany(player => player.GetChildren()),
                Tiles.Select(item => item as IObject),
                Tiles.SelectMany(player => player.GetChildren()),
                Entities.Select(item => item as IObject),
                Entities.SelectMany(player => player.GetChildren())
            );
        }
    }
}
