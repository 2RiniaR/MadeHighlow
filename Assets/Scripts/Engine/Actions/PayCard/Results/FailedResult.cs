using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    /// <summary>
    ///     カードの支払いに失敗した結果
    /// </summary>
    public record FailedResult([NotNull] CardID CardID, FailedReason Reason) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
