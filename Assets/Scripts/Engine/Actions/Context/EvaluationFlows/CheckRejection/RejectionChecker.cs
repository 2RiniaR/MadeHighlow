using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EvaluationFlows.CheckRejection
{
    public class RejectionChecker<TContext> : IRejectionChecker<TContext>
    {
        public RejectionChecker(IEvaluationContext context)
        {
            Context = context;
        }

        [NotNull] private IEvaluationContext Context { get; }

        public void Check(IHistory history, ContextProvider<TContext> contextProvider, RejectHandler onRejected)
        {
            var rejectors = Context.Finder.GetAllComponents<IRejector<TContext>>(history.World).Sort();

            var interrupts = ValueList<Interrupt>.Empty;
            foreach (var rejector in rejectors)
            {
                var context = contextProvider(history, interrupts);
                var interrupt = rejector.Rejection(context);
                if (interrupt == null) continue;
                interrupts = interrupts.Add(interrupt);
            }

            if (interrupts.IsEmpty) return;

            onRejected(interrupts, interrupts[0].ComponentID);
        }
    }
}
