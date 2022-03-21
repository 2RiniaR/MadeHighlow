using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Events;
using RineaR.MadeHighlow.Engine.Events.ComponentAddition;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Components;

namespace RineaR.MadeHighlow.Engine.Actions.ComponentAddition
{
    /// <summary>
    ///     ユニットが自身にエフェクトを追加する
    /// </summary>
    public record ComponentAdditionAction() : Action(ActionType.ComponentAddition)
    {
        [NotNull] public ObjectLocator ObjectLocator { get; init; } = new();
        [CanBeNull] public Component Component { get; init; } = null;

        public override EventTimeline Run(in Session session)
        {
            if (Component == null) return new EventTimeline();
            return new EventTimeline(
                new ComponentAdditionEvent { ObjectLocator = ObjectLocator, Component = Component }
            );
        }
    }
}