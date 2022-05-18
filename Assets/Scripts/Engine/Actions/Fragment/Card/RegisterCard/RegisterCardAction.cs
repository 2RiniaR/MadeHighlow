using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterCard
{
    public record RegisterCardAction([NotNull] PlayerID ParentID, ID AssignedID, [NotNull] Card InitialProps)
    {
        public RegisterCardResult Evaluate(IHistory history)
        {
            return new RegisterCardEvaluator(history, this).Evaluate();
        }
    }
}
