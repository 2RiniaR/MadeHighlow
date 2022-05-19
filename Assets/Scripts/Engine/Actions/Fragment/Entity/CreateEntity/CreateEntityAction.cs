using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateEntity
{
    public record CreateEntityAction([NotNull] Entity InitialProps)
    {
        public CreateEntityResult Evaluate(IHistory history)
        {
            return new CreateEntityEvaluator(history, this).Evaluate();
        }
    }
}
