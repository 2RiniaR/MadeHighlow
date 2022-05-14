namespace RineaR.MadeHighlow.Actions.Valid.InstantHeal
{
    /// <summary>
    ///     治癒を与えることに失敗した結果
    /// </summary>
    public record FailedResult(FailedReason Reason) : InstantHealResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
