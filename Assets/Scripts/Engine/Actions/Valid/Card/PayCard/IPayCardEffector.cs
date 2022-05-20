using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public interface IPayCardEffector : IPriority<IPayCardEffector>
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<PayCardEffect>> EffectsOnPayCard(
            [NotNull] IHistory history,
            [NotNull] PayCardAction action,
            [NotNull] PayCardProcess process
        );
    }
}
