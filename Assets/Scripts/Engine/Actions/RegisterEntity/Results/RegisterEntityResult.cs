namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーを新規登録した結果
    /// </summary>
    public record RegisterEntityResult : Result
    {
        public Entity Registered { get; init; } = new();

        public override World Simulate(in World world)
        {
            return Registered.CreateIn(world);
        }
    }
}