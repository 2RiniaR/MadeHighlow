namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを支払うアクションに対して与える影響
    /// </summary>
    public record PayCardEffect
    {
        /// <summary>
        ///     trueの場合、カードの支払いが免除される
        /// </summary>
        public bool Exempted { get; init; } = false;
    }
}