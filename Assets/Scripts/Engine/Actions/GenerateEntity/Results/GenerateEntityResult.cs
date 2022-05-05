using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GenerateEntityResult : ISimulatable
    {
        [NotNull] public Entity Entity { get; init; } = Entity.Empty;

        public World Simulate(in World world)
        {
            return Entity.Create(world);
        }
    }
}