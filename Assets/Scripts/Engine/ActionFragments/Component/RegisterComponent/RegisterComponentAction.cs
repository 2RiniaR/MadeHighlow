using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.RegisterComponent
{
    public record RegisterComponentAction([NotNull] IAttachableID ParentID, [NotNull] Component InitialProps)
    {
        public RegisterComponentResult Evaluate(IHistory context)
        {
            return new RegisterComponentEvaluator(context, ParentID, InitialProps).Evaluate();
        }
    }
}
