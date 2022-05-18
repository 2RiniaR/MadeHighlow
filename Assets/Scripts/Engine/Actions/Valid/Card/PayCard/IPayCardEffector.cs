using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public interface IPayCardEffector : IComponent
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<PayCardEffect>> EffectsOnPayCard(
            [NotNull] IHistory history,
            [NotNull] Process process
        );
    }
}
