using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IStepOutReactor
    {
        [NotNull]
        public ValueObjectList<StepOutReaction> OnSteppedOut(
            [NotNull] IActionContext session,
            [NotNull] EntityID actor
        );
    }
}