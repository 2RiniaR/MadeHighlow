using System;

namespace RineaR.MadeHighlow.Actions
{
    public class WorldModifier : IWorldModifier
    {
        public WorldModifier(IWorldFinder finder)
        {
            Finder = finder;
        }

        private IWorldFinder Finder { get; }

        public World UpdateCard(World world, Card after)
        {
            var player = Finder.FindPlayer(world, after.OwnerPlayerID) ?? throw new NullReferenceException();
            var modifiedPlayer = player with
            {
                Cards = player.Cards.ReplaceItem(card => card.CardID == after.CardID, after),
            };
            return UpdatePlayer(world, modifiedPlayer);
        }

        public World CreateCard(World world, Card after)
        {
            var player = Finder.FindPlayer(world, after.OwnerPlayerID) ?? throw new NullReferenceException();
            var modifiedPlayer = player with { Cards = player.Cards.Add(after) };
            return UpdatePlayer(world, modifiedPlayer);
        }

        public World DeleteCard(World world, CardID id)
        {
            throw new NotImplementedException();
        }

        public World UpdateEntity(World world, Entity after)
        {
            return world with { Entities = world.Entities.ReplaceItem(tile => tile.EntityID == after.EntityID, after) };
        }

        public World CreateEntity(World world, Entity after)
        {
            return world with { Entities = world.Entities.Add(after) };
        }

        public World DeleteEntity(World world, EntityID id)
        {
            throw new NotImplementedException();
        }

        public World UpdateComponent(World world, Component after)
        {
            var attached = Finder.FindAttachable(world, after.AttachedID) ?? throw new NullReferenceException();
            var modifiedAttached = attached.WithComponents(
                attached.Components.ReplaceItem(card => card.ComponentID == after.ComponentID, after)
            );
            return modifiedAttached.UpdateIn(world);
        }

        public World CreateComponent(World world, Component after)
        {
            var attached = Finder.FindAttachable(world, after.AttachedID) ?? throw new NullReferenceException();
            var modifiedAttached = attached.WithComponents(attached.Components.Add(after));
            return modifiedAttached.UpdateIn(world);
        }

        public World DeleteComponent(World world, ComponentID id)
        {
            throw new NotImplementedException();
        }

        public World UpdateTile(World world, Tile after)
        {
            return world with { Tiles = world.Tiles.ReplaceItem(tile => tile.TileID == after.TileID, after) };
        }

        public World CreateTile(World world, Tile after)
        {
            return world with { Tiles = world.Tiles.Add(after) };
        }

        public World DeleteTile(World world, TileID id)
        {
            throw new NotImplementedException();
        }

        public World UpdatePlayer(World world, Player after)
        {
            return world with
            {
                Players = world.Players.ReplaceItem(player => player.PlayerID == after.PlayerID, after),
            };
        }

        public World CreatePlayer(World world, Player after)
        {
            return world with { Players = world.Players.Add(after) };
        }

        public World DeletePlayer(World world, PlayerID id)
        {
            throw new NotImplementedException();
        }
    }
}
