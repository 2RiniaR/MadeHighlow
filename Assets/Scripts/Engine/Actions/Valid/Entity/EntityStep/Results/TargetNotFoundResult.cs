using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record TargetNotFoundResult([NotNull] EntityStepAction Action) : EntityStepResult;
}
