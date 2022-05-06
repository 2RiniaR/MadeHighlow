namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     IDを確保するアクション
    /// </summary>
    public record AllocateIDAction : Action<AllocateIDResult>
    {
        public override AllocateIDResult Validate(in IActionContext context)
        {
            var latestID = context.World.LatestGeneratedID;
            return new AllocateIDResult { Allocated = ID.From(latestID.InternalValue + 1) };
        }
    }
}