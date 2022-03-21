namespace RineaR.MadeHighlow.Engine.Subjects.Expressions
{
    /// <summary>
    ///     「期限」
    /// </summary>
    public record Duration
    {
        protected Duration(DurationType type)
        {
            Type = type;
        }

        /// <summary>
        ///     種別
        /// </summary>
        public DurationType Type { get; }

        /// <summary>
        ///     無期限の「期限」
        /// </summary>
        public static Duration Unlimited => new(DurationType.Unlimited);
    }
}