using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public interface IAcceptor : IPriority<IAcceptor>
    {
        [CanBeNull]
        public Interrupt<Acceptance> ReserveCommandAcceptance(
            [NotNull] IHistory session,
            [NotNull] Action action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<Acceptance>> collected
        );
    }
}
