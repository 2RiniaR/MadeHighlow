using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries;

namespace RineaR.MadeHighlow.Actions
{
    public record GenerateResult() : Result(ActionType.Generate)
    {
        [NotNull] public Entity Entity { get; init; } = Entity.Empty;

        public override World Simulate(in World world)
        {
            return new CreateEntityQuery
            {
                Value = Entity,
            }.Run(world);
        }
    }
}