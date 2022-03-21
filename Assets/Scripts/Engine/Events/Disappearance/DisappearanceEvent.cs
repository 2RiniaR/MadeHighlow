using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Queries.Objects;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Objects;

namespace RineaR.MadeHighlow.Engine.Events.Disappearance
{
    public record DisappearanceEvent() : Event(EventType.Disappearance)
    {
        [NotNull] public ObjectLocator Actor { get; init; } = new();

        public override World Simulate(in World world)
        {
            return new DeleteObjectQuery
            {
                Locator = Actor,
            }.Run(world);
        }
    }
}