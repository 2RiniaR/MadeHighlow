using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer.Results
{
    /// <summary>
    ///     プレイヤーを新規登録した結果
    /// </summary>
    public record SucceedResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Player RegisteredPlayer
    ) : RegisterPlayerResult
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
