using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection;

namespace RineaR.MadeHighlow.Actions.EvaluationFlows
{
    public interface IEvaluationFlowProvider
    {
        void CheckRejection<TContext>(
            [NotNull] IHistory history,
            [NotNull] ContextProvider<TContext> contextProvider,
            [NotNull] RejectHandler onRejected
        );

        [NotNull]
        ValueList<Event<ReactedResult<IValidResult>>> IterateActions(
            [NotNull] ref IHistory history,
            [NotNull] [ItemNotNull] ValueList<IValidAction> actions
        );
    }
}
