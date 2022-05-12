using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    /// <summary>
    ///     カードを支払った結果
    /// </summary>
    public record SucceedResult([NotNull] CardID PaidCardID) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return PaidCardID.DeleteFrom(world);
        }
    }
}
