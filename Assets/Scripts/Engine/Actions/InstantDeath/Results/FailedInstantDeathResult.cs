namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     即死効果を与えることに失敗した結果
    /// </summary>
    public record FailedInstantDeathResult(FailedInstantDeathReason Reason) : InstantDeathResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
