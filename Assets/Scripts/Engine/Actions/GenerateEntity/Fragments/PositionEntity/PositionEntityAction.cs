using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity.PositionEntity
{
    public record PositionEntityAction([NotNull] EntityID TargetID, [NotNull] Position3D Destination)
    {
        public PositionEntityResult Evaluate(IActionContext context)
        {
            return new PositionEntityEvaluator(context, TargetID, Destination).Evaluate();
        }
    }
}
