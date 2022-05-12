using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    /// <summary>
    ///     カードを支払った結果
    /// </summary>
    public record SucceedResult([NotNull] Card Paid) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return Paid.DeleteFrom(world);
        }
    }
}
