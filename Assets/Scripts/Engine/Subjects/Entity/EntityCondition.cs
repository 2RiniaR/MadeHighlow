using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record EntityCondition
    {
        [CanBeNull] public Position2D Position2D { get; init; }
        [CanBeNull] public Position3D Position3D { get; init; }

        [NotNull]
        [ItemNotNull]
        public ValueObjectList<Entity> Search([NotNull] in World world)
        {
            return Entity.All(world)
                .Where(
                    entity => (Position2D == null || entity.Position3D.To2D() == Position2D) &&
                              (Position3D == null || entity.Position3D == Position3D)
                );
        }
    }
}