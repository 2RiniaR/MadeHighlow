using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries.Objects.Components;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record AddComponentEvent() : Event(ActionType.AddComponent)
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