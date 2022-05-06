namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えることに失敗した結果
    /// </summary>
    public record FailedInstantDeathResult(FailedInstantDeathReason Reason) : InstantDeathResult
    {
        public override World Simulate(in World world)
        {
            return world;
        }
    }
}