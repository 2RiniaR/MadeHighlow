using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard.RegisterCard
{
    /// <summary>
    ///     カードを新規登録した結果
    /// </summary>
    public record FailedResult([NotNull] Card Card, FailedReason Reason) : RegisterCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
