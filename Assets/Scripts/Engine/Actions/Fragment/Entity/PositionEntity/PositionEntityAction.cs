using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionEntity
{
    public record PositionEntityAction([NotNull] EntityID TargetID, [NotNull] Position3D Destination)
    {
        public PositionEntityResult Evaluate(IHistory history)
        {
            return new PositionEntityEvaluator(history, this).Evaluate();
        }
    }
}
