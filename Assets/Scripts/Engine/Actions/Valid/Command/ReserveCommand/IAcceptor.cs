using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public interface IAcceptor : IPriority<IAcceptor>
    {
        [CanBeNull]
        public Interrupt<bool> Acceptance([NotNull] AcceptanceContext context);
    }
}
