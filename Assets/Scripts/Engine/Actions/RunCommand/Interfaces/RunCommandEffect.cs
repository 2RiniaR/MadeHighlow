namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     命令を実行するアクションに対して与える影響
    /// </summary>
    public record RunCommandEffect
    {
        /// <summary>
        ///     trueの場合、行動がキャンセルされる
        /// </summary>
        public bool Canceled { get; init; } = false;
    }
}