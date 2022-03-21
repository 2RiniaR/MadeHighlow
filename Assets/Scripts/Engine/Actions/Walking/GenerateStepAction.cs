using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Actions.Generation;

namespace RineaR.MadeHighlow.Engine.Actions.Walking
{
    public record GenerationStepAction() : StepAction(StepActionType.Generate)
    {
        [NotNull] public GenerationAction Action { get; init; } = new();
    }
}