using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを支払った結果
    /// </summary>
    public record SucceedPayCardResult([NotNull] in CardID PaidCardID) : PayCardResult
    {
        public override World Simulate(in World world)
        {
            return PaidCardID.DeleteFrom(world);
        }
    }
}