using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record GetMultiUnitsQuery
    {
        [CanBeNull] public Position2D Position2D { get; init; } = null;
        [CanBeNull] public Position3D Position3D { get; init; } = null;

        [NotNull]
        public ValueObjectList<Unit> Run([NotNull] in World world)
        {
            return new GetMultiEntitiesQuery
                {
                    Position2D = Position2D,
                    Position3D = Position3D,
                }
                .Run(world)
                .WhereType<Unit>();
        }
    }
}