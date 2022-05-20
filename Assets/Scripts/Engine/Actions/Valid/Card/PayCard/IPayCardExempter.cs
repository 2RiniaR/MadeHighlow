using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.PayCard
{
    public interface IPayCardExempter : IPriority<IPayCardExempter>
    {
        [NotNull]
        public Interrupt<PayCardExemption> PayCardExemption(
            [NotNull] IHistory history,
            [NotNull] PayCardAction action,
            [NotNull] PayCardProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<PayCardExemption>> collected
        );
    }
}
