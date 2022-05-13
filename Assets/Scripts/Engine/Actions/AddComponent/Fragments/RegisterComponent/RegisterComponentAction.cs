using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent.RegisterComponent
{
    public record RegisterComponentAction([NotNull] IAttachableID ParentID, [NotNull] Component InitialProps)
    {
        public RegisterComponentResult Evaluate(IActionContext context)
        {
            return new RegisterComponentEvaluator(context, ParentID, InitialProps).Evaluate();
        }
    }
}
