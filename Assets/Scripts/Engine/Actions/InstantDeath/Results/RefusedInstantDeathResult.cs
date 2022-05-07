namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージが無効化された結果
    /// </summary>
    public record RefusedInstantDeathResult(ComponentID DecidedComponentID) : InstantDeathResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
