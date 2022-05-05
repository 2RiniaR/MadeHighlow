namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     命令を実行するアクションの結果
    /// </summary>
    public abstract record RunOperationResult : Result
    {
        /// <summary>
        ///     発生したリアクションの結果
        /// </summary>
        public ValueObjectList<Result> Reactions { get; init; } = ValueObjectList<Result>.Empty;
    }
}