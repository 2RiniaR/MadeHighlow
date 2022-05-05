using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IStepOutReactor
    {
        [NotNull]
        public ValueObjectList<StepOutReaction> OnSteppedOut(
            [NotNull] in IActionContext session,
            [NotNull] in EntityEnsuredID actor
        );
    }
}