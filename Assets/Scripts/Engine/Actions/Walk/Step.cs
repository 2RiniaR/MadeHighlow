using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Walk
{
    /// <summary>
    ///     フィールド上の1マスの歩行
    /// </summary>
    public record Step
    {
        /// <summary>
        ///     方向
        /// </summary>
        [NotNull]
        public Direction2D Direction2D { get; init; } = new();

        /// <summary>
        ///     追加効果
        /// </summary>
        [NotNull]
        public ValueObjectList<StepAction> Effects { get; init; } = ValueObjectList<StepAction>.Empty;
    }
}