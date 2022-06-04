using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record Process([NotNull] Event<DeleteCard.SucceedResult> DeleteCard)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteCard);
    }
}
