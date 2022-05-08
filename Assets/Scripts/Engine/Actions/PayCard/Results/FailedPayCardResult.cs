using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードの支払いに失敗した結果
    /// </summary>
    public record FailedPayCardResult([NotNull] CardID CardID, FailedPayCardReason Reason) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
