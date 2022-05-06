namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを支払った結果
    /// </summary>
    public record SucceedPayCardResult : PayCardResult
    {
        /// <summary>
        ///     対価として支払ったカードのID
        /// </summary>
        public CardID PaidCardID { get; init; } = new();

        public override World Simulate(in World world)
        {
            return PaidCardID.DeleteFrom(world);
        }
    }
}