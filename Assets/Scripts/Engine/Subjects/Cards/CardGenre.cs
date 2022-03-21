namespace RineaR.MadeHighlow.Engine.Subjects.Cards
{
    /// <summary>
    ///     カードの種別。
    /// </summary>
    public enum CardGenre
    {
        /// <summary>
        ///     共通カード。全ユニットが共通で使用できる。
        /// </summary>
        Common,

        /// <summary>
        ///     固有カード。特定のユニットのみが使用できる。
        /// </summary>
        Unique,

        /// <summary>
        ///     アイテムカード。
        /// </summary>
        Item,

        /// <summary>
        ///     特殊カード。
        /// </summary>
        Special,
    }
}