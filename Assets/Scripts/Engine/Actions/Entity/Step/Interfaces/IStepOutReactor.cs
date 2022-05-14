using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IStepOutReactor
    {
        [NotNull]
        public ValueList<StepOutReaction> OnSteppedOut([NotNull] IActionContext session, [NotNull] EntityID actor);
    }
}
