using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public record DropCardProcess([NotNull] Event<Fragment.DeleteCard.SucceedResult> DeleteCard)
    {
        public Timeline Timeline { get; } = new Timeline().Then(DeleteCard);
    }
}
