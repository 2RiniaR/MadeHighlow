using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity.RegisterEntity
{
    /// <summary>
    ///     エンティティを新規登録した結果
    /// </summary>
    public record SucceedResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Entity RegisteredEntity
    ) : RegisterEntityResult
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
