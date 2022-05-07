using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを支払った結果
    /// </summary>
    public record SucceedPayCardResult([NotNull] CardID PaidCardID) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return PaidCardID.DeleteFrom(world);
        }
    }
}