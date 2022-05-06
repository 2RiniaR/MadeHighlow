using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーを新規登録した結果
    /// </summary>
    public record RegisterPlayerResult([NotNull] in Player RegisteredPlayer) : Result
    {
        public override World Simulate(in World world)
        {
            return RegisteredPlayer.CreateIn(world);
        }
    }
}