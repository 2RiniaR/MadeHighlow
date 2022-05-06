using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ゲーム全体の状態
    /// </summary>
    public record World(
        in ID LatestAllocatedID,
        [NotNull] [ItemNotNull] in ValueObjectList<Player> Players,
        [NotNull] [ItemNotNull] in ValueObjectList<Tile> Tiles,
        [NotNull] [ItemNotNull] in ValueObjectList<Entity> Entities,
        [NotNull] in Turn CurrentTurn,
        [NotNull] [ItemNotNull] in ValueObjectList<Command> ReservedCommands
    )
    {
        [NotNull]
        [ItemNotNull]
        public ValueObjectList<IObject> GetChildren()
        {
            return ValueObjectList.Concat(
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