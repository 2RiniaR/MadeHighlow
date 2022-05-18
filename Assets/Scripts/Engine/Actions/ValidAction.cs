using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     アクション
    /// </summary>
    public abstract record ValidAction
    {
        /// <summary>
        ///     アクションを検証し、結果を返す
        /// </summary>
        [NotNull]
        public abstract ReactedResult EvaluateBase([NotNull] IHistory history);

        [NotNull]
        protected abstract ValidResult EvaluateBodyBase([NotNull] IHistory history);
    }

    public abstract record ValidAction<TResult> : ValidAction where TResult : ValidResult
    {
        public override ReactedResult EvaluateBase(IHistory history)
        {
            return Evaluate(history);
        }

        [NotNull]
        public ReactedResult<TResult> Evaluate([NotNull] IHistory history)
        {
            // TODO: PredictAction, ReactAction の実行優先順位をどうやって決めるか...
            // TODO: Reactionの無限ループ対策をしないといけない。カウンター持ち2体が互いにカウンターし合ったら簡単に無限ループが完成してしまう。
            var predictionActions = Component.GetAllOfTypeFrom<IPredictor>(history.World)
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

            var reactionActions = Component.GetAllOfTypeFrom<IReactor>(history.World)
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

        protected override ValidResult EvaluateBodyBase(IHistory history)
        {
            // Unity 2021.3 では `Covariant return types` をサポートしていないため、命名を同じにできない
            return EvaluateBody(history);
        }

        /// <summary>
        ///     アクションを検証し、結果を返す
        /// </summary>
        [NotNull]
        protected abstract TResult EvaluateBody([NotNull] IHistory history);
    }
}
