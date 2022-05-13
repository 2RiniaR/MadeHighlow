using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity.PositionEntity
{
    public record PositionEntityAction
        ([NotNull] EntityID TargetID, [NotNull] Position3D Destination) : Action<PositionEntityResult>
    {
        public override PositionEntityResult Evaluate(IActionContext context)
        {
            return new PositionEntityEvaluator(context, TargetID, Destination).Evaluate();
        }
    }
}
