using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     アクション
    /// </summary>
    public abstract record Action
    {
        [NotNull]
        public abstract ReactedResult EvaluateBase([NotNull] IActionContext context);

        /// <summary>
        ///     アクションを検証し、結果を返す
        /// </summary>
        [NotNull]
        protected abstract Result EvaluateBodyBase([NotNull] IActionContext context);
    }

    public abstract record Action<TResult> : Action where TResult : Result
    {
        public override ReactedResult EvaluateBase(IActionContext context)
        {
            return Evaluate(context);
        }

        [NotNull]
        public ReactedResult<TResult> Evaluate([NotNull] IActionContext context)
        {
            // TODO: PredictAction, ReactAction の実行優先順位をどうやって決めるか...
            var predictionActions = Component.GetAllOfTypeFrom<IPredictor>(context.World)
                .SelectMany(predictor => predictor.PredictionsOn(this));
            var predictionResults = ValueList<ReactedResult>.Empty;
            foreach (var predictionAction in predictionActions)
            {
                var predictionResult = predictionAction.EvaluateBase(context);
                context = context.Appended(predictionResult);
                predictionResults = predictionResults.Add(predictionResult);
            }

            var bodyResult = EvaluateBody(context);

            var reactionActions = Component.GetAllOfTypeFrom<IReactor>(context.World)
                .SelectMany(predictor => predictor.ReactionsOn(bodyResult));
            var reactionResults = ValueList<ReactedResult>.Empty;
            foreach (var reactionAction in reactionActions)
            {
                var reactionResult = reactionAction.EvaluateBase(context);
                context = context.Appended(reactionResult);
                reactionResults = reactionResults.Add(reactionResult);
            }

            return new ReactedResult<TResult>(predictionResults, bodyResult, predictionResults);
        }

        protected override Result EvaluateBodyBase(IActionContext context)
        {
            // Unity 2021.3 では `Covariant return types` をサポートしていないため、命名を同じにできない
            return EvaluateBody(context);
        }

        /// <summary>
        ///     アクションを検証し、結果を返す
        /// </summary>
        [NotNull]
        protected abstract TResult EvaluateBody([NotNull] IActionContext context);
    }
}
