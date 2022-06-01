namespace RineaR.MadeHighlow.Actions
{
    public class ValidActionRunner
    {
        /*
        [NotNull]
        public ReactedResult<TResult> Evaluate([NotNull] IHistory history, [NotNull] IValidAction action)
        {
            // TODO: PredictAction, ReactAction の実行優先順位をどうやって決めるか...
            // TODO: Reactionの無限ループ対策をしないといけない。カウンター持ち2体が互いにカウンターし合ったら簡単に無限ループが完成してしまう。
            var predictionActions = Context.Finder.GetAllComponents<IPredictor>(history.World)
                .SelectMany(predictor => predictor.PredictionsOn(this));
            var predictionEvents = ValueList<Event<ReactedResult>>.Empty;
            foreach (var predictionAction in predictionActions)
            {
                var predictionResult = predictionAction.EvaluateBase(history);
                history = history.Appended(predictionResult, out var predictionEvent);
                predictionEvents = predictionEvents.Add(predictionEvent);
            }

            var bodyResult = EvaluateBody(history);
            history = history.Appended(bodyResult, out var bodyEvent);

            var reactionActions = Context.Finder.GetAllComponents<IReactor>(history.World)
                .SelectMany(predictor => predictor.ReactionsOn(bodyResult));
            var reactionEvents = ValueList<Event<ReactedResult>>.Empty;
            foreach (var reactionAction in reactionActions)
            {
                var reactionResult = reactionAction.EvaluateBase(history);
                history = history.Appended(reactionResult, out var reactionEvent);
                reactionEvents = reactionEvents.Add(reactionEvent);
            }

            return new ReactedResult<TResult>(predictionEvents, bodyEvent, predictionEvents);
        }
        */
    }
}
