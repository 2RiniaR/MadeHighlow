using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     オブジェクトがフィールド上を歩いて移動するアクション
    /// </summary>
    public record WalkAction() : Action(ActionType.Walk)
    {
        /// <summary>
        ///     行動するユニット
        /// </summary>
        [NotNull]
        public EntityLocator Actor { get; init; } = new();

        /// <summary>
        ///     ステップ
        /// </summary>
        [NotNull]
        public ValueObjectList<StepAction> Steps { get; init; } = ValueObjectList<StepAction>.Empty;

        /// <summary>
        ///     アクションを実行した結果を返す
        /// </summary>
        public WalkResult Run(in ISessionModel session)
        {
            var stepResults = new List<StepResult>();

            foreach (var step in Steps)
            {
                var formattedStep = step with { Actor = Actor };
                var stepResult = formattedStep.Run(session);
                session.Advance(stepResult);
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