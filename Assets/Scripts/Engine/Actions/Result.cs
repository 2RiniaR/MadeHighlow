using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     アクションの結果
    /// </summary>
    public abstract record Result
    {
        /// <summary>
        ///     中身がないアクションの結果
        /// </summary>
        public static Result Empty => new EmptyImpl();

        /// <summary>
        ///     実行後のワールドをシミュレーションする
        /// </summary>
        /// <param name="world">実行前のワールド</param>
        /// <returns>実行後のワールド</returns>
        [NotNull]
        public abstract World Simulate([NotNull] World world);

        private record EmptyImpl : Result
        {
            public override World Simulate(World world)
            {
                return world;
            }
        }
    }
}
