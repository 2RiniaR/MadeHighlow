using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record PayCardProcess([NotNull] Event<DeleteCard.SucceedResult> DeleteCard)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteCard);
    }
}
