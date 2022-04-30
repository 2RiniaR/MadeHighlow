using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries.Objects;

namespace RineaR.MadeHighlow.Actions.Generate
{
    public record GenerateEvent() : Event(ActionType.Generate)
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