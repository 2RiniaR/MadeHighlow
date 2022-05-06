namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒効果を与えることに失敗した結果
    /// </summary>
    public record FailedInstantHealResult : InstantHealResult
    {
        public FailedInstantHealReason Reason { get; init; }

        public override World Simulate(in World world)
        {
            return world;
        }
    }
}