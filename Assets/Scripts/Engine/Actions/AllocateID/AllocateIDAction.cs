namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     IDを確保するアクション
    /// </summary>
    public record AllocateIDAction : Action<AllocateIDResult>
    {
        public override AllocateIDResult Validate(in IActionContext context)
        {
            var latestID = context.World.LatestAllocatedID;
            return new AllocateIDResult(ID.From(latestID.InternalValue + 1));
        }
    }
}