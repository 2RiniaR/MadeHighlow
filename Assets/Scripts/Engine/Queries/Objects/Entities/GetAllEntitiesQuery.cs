using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Entities
{
    public record GetAllEntitiesQuery
    {
        [NotNull]
        public ValueObjectList<Entity> Run([NotNull] in World world)
        {
            return world.Objects.Items.Where(@object => @object.ObjectType == ObjectType.Entity)
                .Cast<Entity>()
                .ToValueObjectList();
        }
    }
}