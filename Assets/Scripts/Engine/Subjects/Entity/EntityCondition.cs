using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティの条件
    /// </summary>
    public sealed record EntityCondition(
        [CanBeNull] Position2D Position2D = null,
        [CanBeNull] Position3D Position3D = null
    )
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Entity> Search([NotNull] World world)
        {
            return Entity.GetAllFrom(world)
                .Where(
                    entity => (Position2D == null || entity.Position3D.To2D() == Position2D) &&
                              (Position3D == null || entity.Position3D == Position3D)
                );
        }
    }
}
