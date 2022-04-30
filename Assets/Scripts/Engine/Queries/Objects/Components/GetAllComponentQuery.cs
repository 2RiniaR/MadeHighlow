using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Components
{
    public record GetAllComponentsQuery
    {
        [NotNull]
        public ValueObjectList<Component> Run([NotNull] in World world)
        {
            return world.Objects.Items.SelectMany(@object => @object.Components.Items).ToValueObjectList();
        }
    }
}