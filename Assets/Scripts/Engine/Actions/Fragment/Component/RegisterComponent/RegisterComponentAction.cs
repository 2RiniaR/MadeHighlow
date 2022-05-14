using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterComponent
{
    public record RegisterComponentAction([NotNull] IAttachableID ParentID, [NotNull] Component InitialProps)
    {
        public RegisterComponentResult Evaluate(IHistory history)
        {
            return new RegisterComponentEvaluator(history, ParentID, InitialProps).Evaluate();
        }
    }
}
