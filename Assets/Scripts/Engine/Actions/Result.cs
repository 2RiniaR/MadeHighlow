using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     アクションの結果
    /// </summary>
    public abstract record Result
    {
        /// <summary>
        ///     実行後のワールドをシミュレーションする
        /// </summary>
        /// <param name="world">実行前のワールド</param>
        /// <returns>実行後のワールド</returns>
        [NotNull]
        public abstract World Simulate([NotNull] World world);
    }
}
