using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Queries.Objects.Components;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Components;

namespace RineaR.MadeHighlow.Engine.Events.ComponentAddition
{
    public record ComponentAdditionEvent() : Event(EventType.ComponentAddition)
    {
        [NotNull] public ObjectLocator ObjectLocator { get; init; } = new();
        [CanBeNull] public Component Component { get; init; } = null;

        public override World Simulate(in World world)
        {
            return new CreateComponentQuery
            {
                ParentLocator = ObjectLocator,
                Value = Component,
            }.Run(world);
        }
    }
}