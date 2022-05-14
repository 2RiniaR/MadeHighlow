using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PositionEntity
{
    public record PositionEntityAction([NotNull] EntityID TargetID, [NotNull] Position3D Destination)
    {
        public PositionEntityResult Evaluate(IHistory history)
        {
            return new PositionEntityEvaluator(history, TargetID, Destination).Evaluate();
        }
    }
}
