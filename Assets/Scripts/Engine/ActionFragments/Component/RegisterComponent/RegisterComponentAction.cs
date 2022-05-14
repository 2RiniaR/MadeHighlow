using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.RegisterComponent
{
    public record RegisterComponentAction([NotNull] IAttachableID ParentID, [NotNull] Component InitialProps)
    {
        public RegisterComponentResult Evaluate(IHistory history)
        {
            return new RegisterComponentEvaluator(history, ParentID, InitialProps).Evaluate();
        }
    }
}
