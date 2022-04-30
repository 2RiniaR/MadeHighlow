using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Generate;

namespace RineaR.MadeHighlow.Actions.Walk
{
    public record GenerationStepAction() : StepAction(StepActionType.Generate)
    {
        [NotNull] public GenerateAction Action { get; init; } = new();
    }
}