using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     オブジェクトがフィールド上を歩いて移動するアクション
    /// </summary>
    public record WalkAction(
        [NotNull] EntityID ActorEntityID,
        [NotNull] [ItemNotNull] ValueList<StepAction> StepActions
    ) : Action<WalkResult>
    {
        public override WalkResult Validate(IActionContext context)
        {
            var stepResults = new List<StepResult>();

            foreach (var stepAction in StepActions)
            {
                var formattedStep = stepAction with { ActorEntityID = ActorEntityID };
                var stepResult = formattedStep.Validate(context);
                context.Appended(stepResult);
                stepResults.Add(stepResult);
            }

            return new WalkResult(ActorEntityID, stepResults.ToValueList());
        }
    }
}
