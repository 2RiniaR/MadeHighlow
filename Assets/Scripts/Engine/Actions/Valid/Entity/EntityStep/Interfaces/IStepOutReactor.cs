using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public interface IStepOutReactor
    {
        [NotNull]
        public ValueList<StepOutReaction> OnSteppedOut([NotNull] IHistory session, [NotNull] EntityID actor);
    }
}
