using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.PositionEntity
{
    public record PositionEntityAction([NotNull] EntityID TargetID, [NotNull] Position3D Destination)
    {
        public PositionEntityResult Evaluate(IHistory history)
        {
            return new PositionEntityEvaluator(history, TargetID, Destination).Evaluate();
        }
    }
}
