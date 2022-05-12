using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard.RegisterCard
{
    /// <summary>
    ///     カードを新規登録した結果
    /// </summary>
    public record SucceedResult(
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
