using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public class WorldFormatter
    {
        [NotNull]
        public World Format([NotNull] in World world)
        {
            var objects = new List<Object>();
            var idGenerator = new IDGenerator();

            foreach (var @object in world.Objects.Items)
                if (@object.ObjectType == ObjectType.Tile)
                {
                    Tile after;
                    (after, idGenerator) = Format((Tile)@object, idGenerator);
                    objects.Add(after);
                }
                else if (@object.ObjectType == ObjectType.Entity)
                {
                    Entity after;
                    (after, idGenerator) = Format((Entity)@object, idGenerator);
                    objects.Add(after);
                }

            var players = new List<Player>();
            foreach (var player in world.Players.Items)
            {
                Player after;
                (after, idGenerator) = Format(player, idGenerator);
                players.Add(after);
            }

            return new World
            {
                Players = players.ToValueObjectList(),
                Objects = objects.ToValueObjectList(),
                CurrentTurn = new Turn(),
                IDGenerator = idGenerator,
            };
        }

        private (Entity, IDGenerator) Format([NotNull] in Entity entity, [NotNull] in IDGenerator idGenerator)
        {
            var (id, idGeneratorAfter) = idGenerator.Generate<Object>();
            return (entity with { ID = id }, idGeneratorAfter);
        }

        private (Player, IDGenerator) Format([NotNull] in Player player, [NotNull] in IDGenerator idGenerator)
        {
            var (id, idGeneratorAfter) = idGenerator.Generate<Player>();
            return (player with { ID = id }, idGeneratorAfter);
        }

        private (Tile, IDGenerator) Format([NotNull] in Tile tile, [NotNull] in IDGenerator idGenerator)
        {
            var (id, idGeneratorAfter) = idGenerator.Generate<Object>();
            return (tile with { ID = id }, idGeneratorAfter);
        }
    }
}