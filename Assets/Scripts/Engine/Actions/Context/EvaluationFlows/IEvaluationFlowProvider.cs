using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.CheckRejection;

namespace RineaR.MadeHighlow.Actions.EvaluationFlows
{
    public interface IEvaluationFlowProvider
    {
        void CheckRejection<TContext>(
            [NotNull] IHistory history,
            [NotNull] ContextProvider<TContext> contextProvider,
            [NotNull] RejectHandler onRejected
        );
    }
}
