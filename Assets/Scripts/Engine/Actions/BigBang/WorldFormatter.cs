using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public class WorldFormatter
    {
        [NotNull]
        public World Format([NotNull] in World world)
        {
            var idGenerator = new IDGenerator();

            var tiles = new List<Tile>();
            foreach (var tile in world.Tiles)
            {
                Tile after;
                (after, idGenerator) = Format(tile, idGenerator);
                tiles.Add(after);
            }

            var entities = new List<Entity>();
            foreach (var tile in world.Entities)
            {
                Entity after;
                (after, idGenerator) = Format(tile, idGenerator);
                entities.Add(after);
            }

            var players = new List<Player>();
            foreach (var player in world.Players)
            {
                Player after;
                (after, idGenerator) = Format(player, idGenerator);
                players.Add(after);
            }

            return new World
            {
                Players = players.ToValueObjectList(),
                Entities = entities.ToValueObjectList(),
                Tiles = tiles.ToValueObjectList(),
                CurrentTurn = new Turn(),
                IDGenerator = idGenerator,
            };
        }

        private (Entity, IDGenerator) Format([NotNull] in Entity entity, [NotNull] in IDGenerator idGenerator)
        {
            var (id, idGeneratorAfter) = idGenerator.Generate();
            return (entity with { ID = id }, idGeneratorAfter);
        }

        private (Player, IDGenerator) Format([NotNull] in Player player, [NotNull] in IDGenerator idGenerator)
        {
            var (id, idGeneratorAfter) = idGenerator.Generate();
            return (player with { ID = id }, idGeneratorAfter);
        }

        private (Tile, IDGenerator) Format([NotNull] in Tile tile, [NotNull] in IDGenerator idGenerator)
        {
            var (id, idGeneratorAfter) = idGenerator.Generate();
            return (tile with { ID = id }, idGeneratorAfter);
        }
    }
}