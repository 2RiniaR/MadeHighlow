using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
{
    public record MoveEntityAction([NotNull] EntityID TargetID, [NotNull] Direction3D Direction)
    {
        public MoveEntityResult Evaluate(IHistory history)
        {
            return new MoveEntityEvaluator(history, TargetID, Direction).Evaluate();
        }
    }
}
