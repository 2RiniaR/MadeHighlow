using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Interact;

namespace RineaR.MadeHighlow.Actions.Walk
{
    public record InteractStepAction() : StepAction(StepActionType.Interact)
    {
        [CanBeNull] public InteractAction Action { get; init; } = null;
    }
}