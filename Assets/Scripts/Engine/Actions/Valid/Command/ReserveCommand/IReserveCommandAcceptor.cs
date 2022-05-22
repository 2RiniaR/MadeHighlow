using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public interface IReserveCommandAcceptor : IPriority<IReserveCommandAcceptor>
    {
        [CanBeNull]
        public Interrupt<ReserveCommandAcceptance> ReserveCommandAcceptance(
            [NotNull] IHistory session,
            [NotNull] ReserveCommandAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<ReserveCommandAcceptance>> collected
        );
    }
}
