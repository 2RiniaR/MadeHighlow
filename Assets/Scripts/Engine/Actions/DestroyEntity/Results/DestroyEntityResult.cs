using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyEntityResult : ISimulatable
    {
        [NotNull] public EntityEnsuredID Actor { get; init; } = new();

        public World Simulate(in World world)
        {
            return Actor.Delete(world);
        }
    }
}