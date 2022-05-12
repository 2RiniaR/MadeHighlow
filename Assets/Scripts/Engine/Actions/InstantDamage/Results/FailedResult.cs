namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    /// <summary>
    ///     ダメージを与えることに失敗した結果
    /// </summary>
    public record FailedResult(FailedReason Reason) : InstantDamageResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
