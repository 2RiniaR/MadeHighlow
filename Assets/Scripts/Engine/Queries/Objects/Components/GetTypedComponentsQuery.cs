using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects.Components
{
    public record GetTypedComponentsQuery<T> where T : class
    {
        [NotNull] public ObjectLocator ParentLocator { get; init; } = new();

        [NotNull]
        public ValueObjectList<T> Run([NotNull] in World world)
        {
            return new GetObjectQuery { Locator = ParentLocator }.Run(world)
                .Components.Items
                .Select(component => component as T)
                .Where(component => component != null)
                .ToValueObjectList();
        }
    }
}