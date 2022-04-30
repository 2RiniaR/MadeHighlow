using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries.Objects;
using RineaR.MadeHighlow.Queries.Objects.Units;

namespace RineaR.MadeHighlow.Actions.CommandUnit
{
    public record CommandUnitEvent() : Event(ActionType.CommandUnit)
    {
        [NotNull] public ObjectLocator Target { get; init; } = new();
        [CanBeNull] public CommandOperation Operation { get; init; } = null;

        public override World Simulate(in World world)
        {
            var unit = new GetUnitQuery { Locator = Target }.Run(world);
            return new UpdateObjectQuery
            {
                Locator = Target,
                Value = unit with { CurrentOperation = Operation },
            }.Run(world);
        }
    }
}