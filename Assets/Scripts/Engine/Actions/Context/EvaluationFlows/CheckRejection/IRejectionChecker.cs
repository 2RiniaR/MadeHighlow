using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EvaluationFlows.CheckRejection
{
    [NotNull]
    public delegate TContext ContextProvider<TContext>(
        [NotNull] IHistory history,
        [NotNull] [ItemNotNull] ValueList<Interrupt> collected
    );

    [NotNull]
    public delegate void RejectHandler(
        [NotNull] [ItemNotNull] ValueList<Interrupt> collected,
        [NotNull] ComponentID rejectedID
    );

    public interface IRejectionChecker<TContext>
    {
        void Check(
            [NotNull] IHistory history,
            [NotNull] ContextProvider<TContext> contextProvider,
            [NotNull] RejectHandler onRejected
        );
    }
}
