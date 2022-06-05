using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection
{
    public interface IRejector<TContext>
    {
        [CanBeNull]
        public Interrupt Rejection([NotNull] TContext context);
    }
}
