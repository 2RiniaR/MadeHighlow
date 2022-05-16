using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityStep
{
    public record EntityStepProcess(
        [NotNull] ValueList<Fragment.MoveEntity.SucceedResult> ClimbResults,
        [NotNull] Fragment.MoveEntity.SucceedResult ShiftResult,
        [NotNull] ValueList<Fragment.MoveEntity.SucceedResult> FallResults
    );
}
