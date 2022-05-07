namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     無期限
    /// </summary>
    public record UnlimitedDuration : Duration
    {
        public override Duration Decrement()
        {
            return this;
        }
    }
}
