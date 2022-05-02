using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record CreateEntityQuery
    {
        [NotNull] public Entity Value { get; init; } = Entity.Empty;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            return world with
            {
                Entities = world.Entities.Add(Value),
            };
        }
    }
}