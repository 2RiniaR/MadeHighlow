using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public record Process(
        [NotNull] Event<Fragment.RegisterCard.SucceedResult> RegisterCard,
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<AddComponent.SucceedResult>>> AddComponents,
        [NotNull] Event<Fragment.PutCard.SucceedResult> PutCard
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(RegisterCard).Then(AddComponents).Then(PutCard);
    }
}
