using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record RemoveComponentProcess([NotNull] Event<DeleteComponent.SucceedResult> DeleteComponentEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteComponentEvent);
    }
}
