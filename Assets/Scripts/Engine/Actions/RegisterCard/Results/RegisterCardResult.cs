using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを新規登録した結果
    /// </summary>
    public record RegisterCardResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Card RegisteredCard
    ) : Result
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = AllocateIDResult.Simulate(currentWorld);
            currentWorld = RegisteredCard.CreateIn(currentWorld);
            return currentWorld;
        }
    }
}
