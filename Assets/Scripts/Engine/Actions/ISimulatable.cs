using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     アクションの結果
    /// </summary>
    public interface ISimulatable
    {
        /// <summary>
        ///     空のアクションの結果
        /// </summary>
        public static ISimulatable Empty => new EmptyImpl();

        /// <summary>
        ///     実行後のワールドをシミュレーションする
        /// </summary>
        /// <param name="world">実行前のワールド</param>
        /// <returns>実行後のワールド</returns>
        [NotNull]
        public World Simulate([NotNull] in World world);

        private record EmptyImpl : ISimulatable
        {
            public World Simulate(in World world)
            {
                return world;
            }
        }
    }
}