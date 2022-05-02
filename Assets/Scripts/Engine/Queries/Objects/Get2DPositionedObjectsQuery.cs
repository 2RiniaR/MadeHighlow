using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Objects
{
    public record Get2DPositionedObjectsQuery
    {
        [NotNull] public Position2D Position2D { get; init; } = new();

        [NotNull]
        public ValueObjectList<Object> Run([NotNull] in World world)
        {
            return world.Objects.Items.FindAll(@object => @object.Position2D == Position2D).ToValueObjectList();
        }
    }
}