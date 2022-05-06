namespace RineaR.MadeHighlow
{
    public record InstantDeathEffect
    {
        /// <summary>
        ///     `true`の場合、即死効果が無効化される
        /// </summary>
        public bool Refused { get; init; } = false;
    }
}