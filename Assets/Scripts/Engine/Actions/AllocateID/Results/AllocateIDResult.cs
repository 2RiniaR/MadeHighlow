namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     IDを確保した結果
    /// </summary>
    public record AllocateIDResult : Result
    {
        public ID Allocated { get; init; } = ID.None;

        public override World Simulate(in World world)
        {
            return world with { LatestGeneratedID = Allocated };
        }
    }
}