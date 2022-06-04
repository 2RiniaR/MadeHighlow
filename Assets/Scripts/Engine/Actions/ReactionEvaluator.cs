using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public class ReactionEvaluator
    {
        public ReactionEvaluator([NotNull] IEvaluationContext context)
        {
            Context = context;
        }

        [NotNull] private IEvaluationContext Context { get; }

        [NotNull]
        public ReactedResult<TResult> Evaluate<TResult>(
            [NotNull] IHistory history,
            [NotNull] IValidAction action,
            [NotNull] Func<IHistory, TResult> bodyRunner
        ) where TResult : IValidResult
        {
            // TODO: PredictAction, ReactAction の実行優先順位をどうやって決めるか...
            // TODO: Reactionの無限ループ対策をしないといけない。カウンター持ち2体が互いにカウンターし合ったら簡単に無限ループが完成してしまう。

            var predictionActions = Context.Finder.GetAllComponents<IPredictor>(history.World)
                .SelectMany(predictor => predictor.PredictionsOn(action));
            var predictionEvents = ValueList<Event<ReactedResult<IValidResult>>>.Empty;
            foreach (var predictionAction in predictionActions)
            {
                var predictionResult = Context.Actions.Run(history, predictionAction);
                history = history.Appended(predictionResult, out var predictionEvent);
                predictionEvents = predictionEvents.Add(predictionEvent);
            }

            var bodyResult = bodyRunner(history);
            history = history.Appended(bodyResult, out var bodyEvent);

            var reactionActions = Context.Finder.GetAllComponents<IReactor>(history.World)
                .SelectMany(predictor => predictor.ReactionsOn(bodyResult));
            var reactionEvents = ValueList<Event<ReactedResult<IValidResult>>>.Empty;
            foreach (var reactionAction in reactionActions)
            {
                var reactionResult = Context.Actions.Run(history, reactionAction);
                history = history.Appended(reactionResult, out var reactionEvent);
                reactionEvents = reactionEvents.Add(reactionEvent);
            }

            return new ReactedResult<TResult>(predictionEvents, bodyEvent, predictionEvents);
        }
    }
}
