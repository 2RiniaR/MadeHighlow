using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IWorldFinder
    {
        [NotNull]
        [ItemNotNull]
        ValueList<Tile> GetAllTiles([NotNull] World world);

        [CanBeNull]
        Tile FindTile([NotNull] World world, [NotNull] TileID id);

        [CanBeNull]
        Tile FindTile([NotNull] World world, [NotNull] Position2D position2D);

        [NotNull]
        [ItemNotNull]
        ValueList<Player> GetAllPlayers([NotNull] World world);

        [CanBeNull]
        Player FindPlayer([NotNull] World world, [NotNull] PlayerID id);

        [NotNull]
        [ItemNotNull]
        ValueList<Entity> GetAllEntities([NotNull] World world);

        [CanBeNull]
        Entity FindEntity([NotNull] World world, [NotNull] EntityID id);

        [NotNull]
        [ItemNotNull]
        ValueList<Entity> SearchEntities([NotNull] World world, [NotNull] EntityCondition condition);

        [NotNull]
        [ItemNotNull]
        ValueList<T> GetAllComponents<T>([NotNull] World world) where T : class;

        [CanBeNull]
        Component FindComponent([NotNull] World world, [NotNull] ComponentID id);

        [NotNull]
        [ItemNotNull]
        ValueList<Card> GetAllCards([NotNull] World world);

        [CanBeNull]
        Card FindCard([NotNull] World world, [NotNull] CardID id);

        [NotNull]
        [ItemNotNull]
        ValueList<Unit> GetAllUnits([NotNull] World world);

        [CanBeNull]
        Unit FindUnit([NotNull] World world, [NotNull] UnitID id);

        [CanBeNull]
        IAttachable FindAttachable([NotNull] World world, [NotNull] IAttachableID id);
    }
}
