using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.RemoveComponent
{
    public record Process([NotNull] Event<Fragment.DeleteComponent.SucceedResult> DeleteComponentEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteComponentEvent);
    }
}
