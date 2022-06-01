using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record World(
        ID LatestAllocatedID,
        [NotNull] [ItemNotNull] ValueList<Player> Players,
        [NotNull] [ItemNotNull] ValueList<Tile> Tiles,
        [NotNull] [ItemNotNull] ValueList<Entity> Entities,
        [NotNull] Turn CurrentTurn,
        [NotNull] [ItemNotNull] ValueList<Command> ReservedCommands
    );
}
