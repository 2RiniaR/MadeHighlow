namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えることに失敗した結果
    /// </summary>
    public record FailedInstantDeathResult : InstantDeathResult
    {
        public FailedInstantDeathReason Reason { get; init; }

        public override World Simulate(in World world)
        {
            return world;
        }
    }
}