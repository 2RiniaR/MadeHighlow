using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを対価として支払うアクションの結果
    /// </summary>
    public record PayCardResult : ISimulatable
    {
        /// <summary>
        ///     対価として支払ったカードのID
        /// </summary>
        public CardEnsuredID PaidCardID { get; init; } = new();

        public World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}