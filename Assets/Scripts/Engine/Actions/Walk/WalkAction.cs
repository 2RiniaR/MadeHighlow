using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     オブジェクトがフィールド上を歩いて移動するアクション
    /// </summary>
    public record WalkAction : Action<WalkResult>
    {
        /// <summary>
        ///     行動するオブジェクト
        /// </summary>
        [NotNull]
        public EntityID Actor { get; init; } = new();

        /// <summary>
        ///     ステップ
        /// </summary>
        [NotNull]
        public ValueObjectList<StepAction> Steps { get; init; } = ValueObjectList<StepAction>.Empty;


        /// <summary>
        ///     アクションを実行した結果を返す
        /// </summary>
        [NotNull]
        public override WalkResult Validate([NotNull] in IActionContext context)
        {
            var stepResults = new List<StepResult>();

            foreach (var step in Steps)
            {
                var formattedStep = step with { Actor = Actor };
                var stepResult = formattedStep.Validate(context);
                context.Appended(stepResult);
                stepResults.Add(stepResult);
            }

            return new WalkResult
            {
                Actor = Actor,
                Steps = stepResults.ToValueObjectList(),
            };
        }
    }
}