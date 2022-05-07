using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IStepInReactor
    {
        [NotNull]
        public ValueObjectList<StepInReaction> OnSteppedIn([NotNull] IActionContext session, [NotNull] EntityID actor);
    }
}
