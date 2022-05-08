using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティを新規登録した結果
    /// </summary>
    public record RegisterEntityResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Entity RegisteredEntity
    ) : Result
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = AllocateIDResult.Simulate(currentWorld);
            currentWorld = RegisteredEntity.CreateIn(currentWorld);
            return currentWorld;
        }
    }
}
