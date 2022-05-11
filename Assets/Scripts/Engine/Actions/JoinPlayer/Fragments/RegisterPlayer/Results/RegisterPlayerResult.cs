using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーを新規登録した結果
    /// </summary>
    public record RegisterPlayerResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Player RegisteredPlayer
    ) : Result
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = AllocateIDResult.Simulate(currentWorld);
            currentWorld = RegisteredPlayer.CreateIn(currentWorld);
            return currentWorld;
        }
    }
}
