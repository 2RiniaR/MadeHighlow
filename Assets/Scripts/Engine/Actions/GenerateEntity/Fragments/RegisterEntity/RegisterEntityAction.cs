using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity.RegisterEntity
{
    public record RegisterEntityAction([NotNull] Entity InitialProps) : Action<RegisterEntityResult>
    {
        public override RegisterEntityResult Evaluate(IActionContext context)
        {
            return new RegisterEntityEvaluator(context, InitialProps).Evaluate();
        }
    }
}
