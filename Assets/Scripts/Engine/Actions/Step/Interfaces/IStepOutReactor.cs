using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IStepOutReactor
    {
        [NotNull]
        public ValueObjectList<StepOutReaction> OnSteppedOut(
            [NotNull] in ISessionModel session,
            [NotNull] in ObjectLocator actor,
            [NotNull] in ObjectLocator reactor
        );
    }
}