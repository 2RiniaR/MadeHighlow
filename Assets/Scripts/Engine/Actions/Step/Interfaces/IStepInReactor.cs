using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IStepInReactor
    {
        [NotNull]
        public ValueObjectList<StepInReaction> OnSteppedIn(
            [NotNull] in IActionContext session,
            [NotNull] in EntityEnsuredID actor
        );
    }
}