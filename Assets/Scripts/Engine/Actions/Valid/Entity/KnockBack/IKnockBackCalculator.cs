using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public interface IKnockBackCalculator : IPriority<IKnockBackCalculator>
    {
        [CanBeNull]
        [ItemNotNull]
        public ValueList<Interrupt<KnockBackCalculation>> KnockBackCalculations(
            [NotNull] IHistory history,
            [NotNull] KnockBackAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<KnockBackCalculation>> collected
        );
    }
}
