using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.ReserveCommand;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    [NotNull]
    public delegate CalculationContext ContextProvider(
        [NotNull] IHistory history,
        [NotNull] [ItemNotNull] ValueList<Interrupt<Calculation>> collected
    );

    [NotNull]
    public delegate void ApplyHandler([NotNull] Acceptance acceptance);

    public class CalculationChecker
    {
        public CalculationChecker([NotNull] IEvaluationContext context)
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
