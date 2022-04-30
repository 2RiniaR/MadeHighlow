using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.BigBang
{
    public class WorldFormatter
    {
        [NotNull]
        public World Format([NotNull] in World world)
        {
            var objects = new List<Object>();

            foreach (var @object in world.Objects.Items)
                if (@object.ObjectType == ObjectType.Tile)
                    objects.Add(Format((Tile)@object));
                else if (@object.ObjectType == ObjectType.Entity)
                    objects.Add(Format((Entity)@object));

            return new World
            {
                Players = world.Players.Items.Select(Format).ToValueObjectList(),
                Objects = objects.ToValueObjectList(),
                CurrentTurn = new Turn(),
            };
        }

        [NotNull]
        private Entity Format([NotNull] Entity entity)
        {
            return entity with { ID = ID<Object>.None };
        }

        [NotNull]
        private Player Format([NotNull] Player player)
        {
            return player with { ID = ID<Player>.None };
        }

        [NotNull]
        private Tile Format([NotNull] Tile tile)
        {
            return tile with { ID = ID<Object>.None };
        }
    }
}