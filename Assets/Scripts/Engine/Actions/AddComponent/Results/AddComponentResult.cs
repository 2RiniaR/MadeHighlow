using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     コンポーネントを追加するアクションの結果
    /// </summary>
    public record AddComponentResult : Result
    {
        /// <summary>
        ///     追加されたコンポーネント
        /// </summary>
        [NotNull]
        public Component AddedComponent { get; init; } = Component.Empty;

        public override World Simulate(in World world)
        {
            return AddedComponent.CreateIn(world);
        }
    }
}