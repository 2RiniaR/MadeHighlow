using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedStepResult() : StepResult(StepResultCode.Succeed)
    {
        /// <summary>
        ///     行動したユニット
        /// </summary>
        [NotNull]
        public EntityEnsuredID Actor { get; init; } = new();

        /// <summary>
        ///     移動した方向
        /// </summary>
        [NotNull]
        public Direction2D Direction2D { get; init; } = new();

        /// <summary>
        ///     移動元から踏み出した際のリアクション
        /// </summary>
        [NotNull]
        public ValueObjectList<StepOutReaction> StepOutReactions { get; init; } =
            ValueObjectList<StepOutReaction>.Empty;

        /// <summary>
        ///     移動先へ踏み入った際のリアクション
        /// </summary>
        [NotNull]
        public ValueObjectList<StepInReaction> StepInReactions { get; init; } = ValueObjectList<StepInReaction>.Empty;

        /// <summary>
        ///     追加効果の結果
        /// </summary>
        [NotNull]
        public ValueObjectList<ISimulatable> AfterActionResults { get; init; } = ValueObjectList<ISimulatable>.Empty;

        /// <summary>
        ///     使用可能だった移動コスト
        /// </summary>
        [NotNull]
        public StepCost AvailableCost { get; init; } = new();

        public World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}