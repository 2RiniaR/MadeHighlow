using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Exceptions;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Entities;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Tiles;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Engine.Actions.BigBang
{
    public class WorldFormatter
    {
        [NotNull]
        public World Format([NotNull] in World world)
        {
            var objects = ImmutableList.CreateBuilder<Object>();

            foreach (var @object in world.Objects)
                if (@object.ObjectType == ObjectType.Tile)
                    objects.Add(Format(@object as Tile ?? throw new DataTypeContradictionException()));
                else if (@object.ObjectType == ObjectType.Entity)
                    objects.Add(Format(@object as Entity ?? throw new DataTypeContradictionException()));

            return new World
            {
                Players = world.Players.Select(Format).ToImmutableList(),
                Objects = objects.ToImmutable(),
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