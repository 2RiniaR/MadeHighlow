using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record DropCardAction([NotNull] CardID TargetID) : Action<DropCardResult>
    {
        public override DropCardResult Evaluate(IActionContext context)
        {
            return new DropCardEvaluator(context, TargetID).Evaluate();
        }
    }
}
