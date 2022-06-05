using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection
{
    public interface IRejector<in TContext>
    {
        [CanBeNull]
        public Interrupt Rejection([NotNull] TContext context);
    }
}
