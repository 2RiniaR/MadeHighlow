namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒効果を与えることに失敗した結果
    /// </summary>
    public record FailedInstantHealResult(FailedInstantHealReason Reason) : InstantHealResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}