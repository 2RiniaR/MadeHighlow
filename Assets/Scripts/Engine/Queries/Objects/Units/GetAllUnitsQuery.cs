using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Units
{
    public record GetAllUnitsQuery
    {
        [NotNull]
        public ValueObjectList<Unit> Run([NotNull] in World world)
        {
            return world.Objects.Items.Where(@object => @object.ObjectType == ObjectType.Unit)
                .Cast<Unit>()
                .ToValueObjectList();
        }
    }
}