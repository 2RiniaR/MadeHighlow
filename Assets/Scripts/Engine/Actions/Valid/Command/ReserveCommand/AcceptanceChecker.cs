using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    [NotNull]
    public delegate AcceptanceContext ContextProvider(
        [NotNull] IHistory history,
        [NotNull] [ItemNotNull] ValueList<Interrupt<bool>> collected
    );

    [NotNull]
    public delegate void ApplyHandler([NotNull] Acceptance acceptance);

    public class AcceptanceChecker
    {
        public AcceptanceChecker([NotNull] IEvaluationContext context)
        {
            Context = context;
        }

        [NotNull] private IEvaluationContext Context { get; }

        public void Check(IHistory history, ContextProvider contextProvider, ApplyHandler onApplied)
        {
            var acceptors = Context.Finder.GetAllComponents<IAcceptor>(history.World).Sort();

            var acceptances = ValueList<Interrupt<bool>>.Empty;
            foreach (var acceptor in acceptors)
            {
                var context = contextProvider(history, acceptances);
                var interrupt = acceptor.Acceptance(context);
                if (interrupt == null) continue;
                acceptances = acceptances.Add(interrupt);
            }

            if (acceptances.IsEmpty) return;

            var applied = acceptances[0];
            onApplied(new Acceptance(applied.ComponentID, applied.Content));
        }
    }
}
