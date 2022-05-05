namespace RineaR.MadeHighlow
{
    public interface IIdentified : IObject
    {
        /// <summary>
        ///     セッション内での識別子
        /// </summary>
        public ID ID { get; init; }
    }
}