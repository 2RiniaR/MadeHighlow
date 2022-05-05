namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを対価として支払うアクションの結果
    /// </summary>
    public abstract record PayCardResult : Result
    {
        /// <summary>
        ///     発生したリアクションの結果
        /// </summary>
        public ValueObjectList<Result> Reactions { get; init; } = ValueObjectList<Result>.Empty;
    }
}