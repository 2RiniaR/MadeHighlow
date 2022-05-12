namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     IDを確保するアクション
    /// </summary>
    public record AllocateIDAction : Action<AllocateIDResult>
    {
        public override AllocateIDResult Validate(IActionContext context)
        {
            var latestID = context.World.LatestAllocatedID;
            return new AllocateIDResult(ID.From(latestID.InternalValue + 1));
        }
    }
}
