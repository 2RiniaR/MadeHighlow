using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries.Objects;

namespace RineaR.MadeHighlow.Actions
{
    public record GenerateResult() : Result(ActionType.Generate)
    {
        [CanBeNull] public Object Object { get; init; } = null;

        public override World Simulate(in World world)
        {
            return new CreateObjectQuery
            {
                Value = Object,
            }.Run(world);
        }
    }
}