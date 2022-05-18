using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public record Process(
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<RemoveComponent.SucceedResult>>> RemoveComponents,
        [NotNull] Event<Fragment.DeleteCard.SucceedResult> DeleteCard
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(RemoveComponents).Then(DeleteCard);
    }
}
