using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public record Process([NotNull] ValueList<Event<ReactedResult<EntityStep.SucceedResult>>> EntityStepEvents)
    {
        public Timeline Timeline { get; } = new Timeline().Then(EntityStepEvents);
    }
}
