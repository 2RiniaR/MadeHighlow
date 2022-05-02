using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record GetMultiEntityComponentsQuery
    {
        [NotNull]
        public ValueObjectList<EntityComponent> Run([NotNull] in World world)
        {
            return world.Entities.SelectMany(entity => entity.Components);
        }
    }
}