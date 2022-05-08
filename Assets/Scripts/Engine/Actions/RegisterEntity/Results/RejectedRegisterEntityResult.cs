using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティを新規登録した結果
    /// </summary>
    public record RejectedRegisterEntityResult
        ([NotNull] Entity Entity, [NotNull] ComponentID RejectedComponentID) : RegisterEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
