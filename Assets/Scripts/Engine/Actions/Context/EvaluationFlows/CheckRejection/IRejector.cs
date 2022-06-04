using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EvaluationFlows.CheckRejection
{
    public interface IRejector<TContext>
    {
        [CanBeNull]
        public Interrupt Rejection([NotNull] TContext context);
    }
}
