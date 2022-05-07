namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードによる行動の早さ
    /// </summary>
    public enum Quickness
    {
        /// <summary>
        ///     （デバッグ用）最も最後に行動する。
        /// </summary>
        LastOnDebug,

        /// <summary>
        ///     最も最後に行動する。
        /// </summary>
        Last,

        /// <summary>
        ///     優先度1。赤色の印で表される。
        /// </summary>
        Red,

        /// <summary>
        ///     優先度2。黄色の印で表される。
        /// </summary>
        Yellow,

        /// <summary>
        ///     優先度3。緑色の印で表される。
        /// </summary>
        Green,

        /// <summary>
        ///     優先度4。白色の印で表される。
        /// </summary>
        White,

        /// <summary>
        ///     最も最初に行動する。
        /// </summary>
        First,

        /// <summary>
        ///     （デバッグ用）最も最初に行動する。
        /// </summary>
        FirstOnDebug,
    }
}
