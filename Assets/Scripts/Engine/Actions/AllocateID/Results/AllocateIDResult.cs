namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     IDを確保した結果
    /// </summary>
    public record AllocateIDResult(ID AllocatedID) : Result
    {
        public override World Simulate(World world)
        {
            return world with { LatestAllocatedID = AllocatedID };
        }
    }
}