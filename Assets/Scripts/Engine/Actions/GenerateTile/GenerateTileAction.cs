using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record GenerateTileAction([NotNull] Tile InitialStatus) : Action<GenerateTileResult>
    {
        public override GenerateTileResult Evaluate(IActionContext context)
        {
            return new GenerateTileEvaluator(context, InitialStatus).Evaluate();
        }
    }
}
