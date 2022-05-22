using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record DropCardProcess([NotNull] Event<DeleteCard.SucceedResult> DeleteCard)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteCard);
    }
}
