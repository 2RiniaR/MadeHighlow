using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Queries.Objects;
using RineaR.MadeHighlow.Engine.Queries.Objects.Units;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Units;

namespace RineaR.MadeHighlow.Engine.Events.Command
{
    public record CommandEvent() : Event(EventType.Command)
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