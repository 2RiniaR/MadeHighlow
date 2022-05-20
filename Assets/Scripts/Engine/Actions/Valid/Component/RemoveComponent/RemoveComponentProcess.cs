using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.RemoveComponent
{
    public record RemoveComponentProcess([NotNull] Event<Fragment.DeleteComponent.SucceedResult> DeleteComponentEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteComponentEvent);
    }
}
