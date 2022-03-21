using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Queries.Objects;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;

namespace RineaR.MadeHighlow.Engine.Events.Generation
{
    public record GenerationEvent() : Event(EventType.Generation)
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