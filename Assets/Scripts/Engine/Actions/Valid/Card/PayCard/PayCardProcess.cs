using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public record PayCardProcess([NotNull] Event<Fragment.DeleteCard.SucceedResult> DeleteCard)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteCard);
    }
}
