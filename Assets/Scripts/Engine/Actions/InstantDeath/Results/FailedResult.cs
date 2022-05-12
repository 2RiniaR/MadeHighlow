namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    /// <summary>
    ///     即死効果を与えることに失敗した結果
    /// </summary>
    public record FailedResult(FailedReason Reason) : InstantDeathResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
