namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えることに失敗した結果
    /// </summary>
    public record FailedInstantDamageResult(in FailedInstantDamageReason Reason) : InstantDamageResult
    {
        public override World Simulate(in World world)
        {
            return world;
        }
    }
}