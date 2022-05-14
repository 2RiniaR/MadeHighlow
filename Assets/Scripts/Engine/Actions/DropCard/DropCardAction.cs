using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record DropCardAction([NotNull] CardID TargetID) : Action<DropCardResult>
    {
        protected override DropCardResult EvaluateBody(IActionContext context)
        {
            return new DropCardEvaluator(context, TargetID).Evaluate();
        }
    }
}
