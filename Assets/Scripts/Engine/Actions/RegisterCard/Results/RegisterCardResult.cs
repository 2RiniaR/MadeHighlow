namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーを新規登録した結果
    /// </summary>
    public record RegisterCardResult : Result
    {
        public Card Registered { get; init; }

        public override World Simulate(in World world)
        {
            return Registered.CreateIn(world);
        }
    }
}