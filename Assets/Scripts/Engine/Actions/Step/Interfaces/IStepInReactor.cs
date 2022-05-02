using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IStepInReactor
    {
        [NotNull]
        public ValueObjectList<StepInReaction> OnSteppedIn(
            [NotNull] in ISessionModel session,
            [NotNull] in ObjectLocator actor,
            [NotNull] in ObjectLocator reactor
        );
    }
}