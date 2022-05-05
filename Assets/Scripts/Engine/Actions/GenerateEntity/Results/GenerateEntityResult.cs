using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GenerateEntityResult : Result
    {
        [NotNull] public Entity Entity { get; init; } = Entity.Empty;

        public override World Simulate(in World world)
        {
            return Entity.CreateIn(world);
        }
    }
}