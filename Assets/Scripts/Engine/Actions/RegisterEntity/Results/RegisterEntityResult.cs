using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティを新規登録した結果
    /// </summary>
    public record RegisterEntityResult([NotNull] in Entity RegisteredEntity) : Result
    {
        public override World Simulate(in World world)
        {
            return RegisteredEntity.CreateIn(world);
        }
    }
}