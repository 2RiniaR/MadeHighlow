using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     ゲームの状態に影響を及ぼす「行動の結果」
    /// </summary>
    public abstract record Result(ActionType Type)
    {
        /// <summary>
        ///     アクションの種別
        /// </summary>
        public ActionType Type { get; } = Type;

        /// <summary>
        ///     実行後のワールドをシミュレーションする
        /// </summary>
        /// <param name="world">実行前のワールド</param>
        /// <returns>実行後のワールド</returns>
        [NotNull]
        public abstract World Simulate([NotNull] in World world);
    }
}