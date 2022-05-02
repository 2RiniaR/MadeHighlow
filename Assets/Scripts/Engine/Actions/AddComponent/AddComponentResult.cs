using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries.Objects.Components;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     コンポーネントを追加するアクションの結果
    /// </summary>
    public record AddComponentResult() : Result(ActionType.AddComponent)
    {
        /// <summary>
        ///     コンポーネントが追加された対象
        /// </summary>
        [NotNull]
        public ObjectLocator Target { get; init; } = new();

        /// <summary>
        ///     追加されたコンポーネント
        /// </summary>
        [NotNull]
        public Component AddedComponent { get; init; } = new EmptyComponent();

        public override World Simulate(in World world)
        {
            var query = new CreateComponentQuery
            {
                ParentLocator = Target,
                Value = AddedComponent,
            };

            return query.Run(world);
        }
    }
}