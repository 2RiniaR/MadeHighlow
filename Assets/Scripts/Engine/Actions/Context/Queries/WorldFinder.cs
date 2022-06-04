using System;

namespace RineaR.MadeHighlow.Actions
{
    public class WorldFinder : IWorldFinder
    {
        public ValueList<Tile> GetAllTiles(World world)
        {
            return world.Tiles;
        }

        public Tile FindTile(World world, TileID id)
        {
            return GetAllTiles(world).Find(tile => tile.TileID == id);
        }

        public ValueList<Player> GetAllPlayers(World world)
        {
            return world.Players;
        }

        public Player FindPlayer(World world, PlayerID id)
        {
            return GetAllPlayers(world).Find(player => player.PlayerID == id);
        }

        public ValueList<Entity> GetAllEntities(World world)
        {
            return world.Entities;
        }

        public Entity FindEntity(World world, EntityID id)
        {
            return GetAllEntities(world).Find(entity => entity.EntityID == id);
        }

        public ValueList<Entity> SearchEntities(World world, EntityCondition condition)
        {
            return GetAllEntities(world)
                .Where(
                    entity => (condition.Position2D == null || entity.Position3D.To2D() == condition.Position2D) &&
                              (condition.Position3D == null || entity.Position3D == condition.Position3D)
                );
        }

        public ValueList<T> GetAllComponents<T>(World world) where T : class
        {
            throw new NotImplementedException();
        }

        public Component FindComponent(World world, ComponentID id)
        {
            throw new NotImplementedException();
        }

        public ValueList<Card> GetAllCards(World world)
        {
            return GetAllPlayers(world).SelectMany(player => player.Cards);
        }

        public Card FindCard(World world, CardID id)
        {
            return GetAllCards(world).Find(card => card.CardID == id);
        }

        public ValueList<Unit> GetAllUnits(World world)
        {
            return GetAllPlayers(world).SelectMany(player => player.Units);
        }

        public Unit FindUnit(World world, UnitID id)
        {
            return GetAllUnits(world).Find(unit => unit.UnitID == id);
        }

        public IAttachable FindAttachable(World world, IAttachableID id)
        {
            throw new NotImplementedException();
        }
    }
}
