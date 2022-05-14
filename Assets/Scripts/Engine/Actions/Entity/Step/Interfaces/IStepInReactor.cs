using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IStepInReactor
    {
        [NotNull]
        public ValueList<StepInReaction> OnSteppedIn([NotNull] IHistory session, [NotNull] EntityID actor);
    }
}
