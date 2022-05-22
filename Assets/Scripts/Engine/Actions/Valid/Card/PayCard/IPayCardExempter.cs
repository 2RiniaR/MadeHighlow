using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public interface IPayCardExempter : IPriority<IPayCardExempter>
    {
        [CanBeNull]
        public Interrupt<PayCardExemption> PayCardExemption(
            [NotNull] IHistory history,
            [NotNull] PayCardAction action,
            [NotNull] PayCardProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<PayCardExemption>> collected
        );
    }
}
