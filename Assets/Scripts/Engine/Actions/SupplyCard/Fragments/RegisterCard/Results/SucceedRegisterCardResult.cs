using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを新規登録した結果
    /// </summary>
    public record SucceedRegisterCardResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Card RegisteredCard
    ) : RegisterCardResult
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
