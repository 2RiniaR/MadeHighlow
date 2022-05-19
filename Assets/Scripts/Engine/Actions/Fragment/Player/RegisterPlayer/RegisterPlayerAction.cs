using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterPlayer
{
    public record RegisterPlayerAction(ID AssignedID, [NotNull] Player InitialProps)
    {
        public RegisterPlayerResult Evaluate(IHistory history)
        {
            return new RegisterPlayerEvaluator(history, this).Evaluate();
        }
    }
}
