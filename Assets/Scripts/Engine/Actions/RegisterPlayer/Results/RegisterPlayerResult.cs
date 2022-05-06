namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーを新規登録した結果
    /// </summary>
    public record RegisterPlayerResult : Result
    {
        public Player Registered { get; init; } = new();

        public override World Simulate(in World world)
        {
            return Registered.CreateIn(world);
        }
    }
}