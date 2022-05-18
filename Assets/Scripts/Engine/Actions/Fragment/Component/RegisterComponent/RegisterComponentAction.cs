using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterComponent
{
    public record RegisterComponentAction(
        [NotNull] IAttachableID ParentID,
        ID AssignedID,
        [NotNull] Component InitialProps
    )
    {
        public RegisterComponentResult Evaluate(IHistory history)
        {
            return new RegisterComponentEvaluator(history, this).Evaluate();
        }
    }
}
