using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record DestinationNotFoundResult([NotNull] EntityStepAction Action) : EntityStepResult;
}
