using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IStepInReactor
    {
        [NotNull]
        public ValueList<StepInReaction> OnSteppedIn([NotNull] IActionContext session, [NotNull] EntityID actor);
    }
}
