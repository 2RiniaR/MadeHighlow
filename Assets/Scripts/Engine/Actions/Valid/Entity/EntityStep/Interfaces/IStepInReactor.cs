using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public interface IStepInReactor
    {
        [NotNull]
        public ValueList<StepInReaction> OnSteppedIn([NotNull] IHistory session, [NotNull] EntityID actor);
    }
}
