using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Events;
using RineaR.MadeHighlow.Engine.Events.Disappearance;
using RineaR.MadeHighlow.Engine.Subjects.Objects;

namespace RineaR.MadeHighlow.Engine.Actions.Disappearance
{
    public record DisappearanceAction() : Action(ActionType.Disappearance)
    {
        [NotNull] public ObjectLocator ObjectID { get; init; } = new();

        public override EventTimeline Run(in Session session)
        {
            return new EventTimeline(new DisappearanceEvent { Actor = ObjectID });
        }
    }
}