using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Actions.Interaction;

namespace RineaR.MadeHighlow.Engine.Actions.Walking
{
    public record InteractStepAction() : StepAction(StepActionType.Interact)
    {
        [CanBeNull] public InteractionAction Action { get; init; } = null;
    }
}