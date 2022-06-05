using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection
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

            var rejections = ValueList<Interrupt>.Empty;
            foreach (var rejector in rejectors)
            {
                var context = contextProvider(history, rejections);
                var interrupt = rejector.Rejection(context);
                if (interrupt == null) continue;
                rejections = rejections.Add(interrupt);
            }

            if (rejections.IsEmpty) return;

            var rejection = new Rejection(rejections[0].ComponentID);
            onRejected(rejection);
        }
    }
}
