namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えることに失敗した結果
    /// </summary>
    public record FailedInstantDamageResult(FailedInstantDamageReason Reason) : InstantDamageResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}