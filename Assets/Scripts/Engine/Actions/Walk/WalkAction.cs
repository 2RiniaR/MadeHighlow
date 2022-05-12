using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     オブジェクトがフィールド上を歩いて移動するアクション
    /// </summary>
    public record WalkAction(
        [NotNull] EntityID ActorEntityID,
        [NotNull] [ItemNotNull] ValueList<StepAction> StepActions
    ) : Action<WalkResult>
    {
        public override WalkResult Evaluate(IActionContext context)
        {
            var stepResults = new List<StepResult>();

            foreach (var stepAction in StepActions)
            {
                var formattedStep = stepAction with { ActorEntityID = ActorEntityID };
                var stepResult = formattedStep.Evaluate(context);
                context.Appended(stepResult);
                stepResults.Add(stepResult);
            }

            return new WalkResult(ActorEntityID, stepResults.ToValueList());
        }
    }
}
