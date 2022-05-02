using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries;

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
        public EntityLocator Target { get; init; } = new();

        /// <summary>
        ///     追加されたコンポーネント
        /// </summary>
        [NotNull]
        public EntityComponent AddedEntityComponent { get; init; } = EntityComponent.Empty;

        public override World Simulate(in World world)
        {
            var query = new CreateEntityComponentQuery
            {
                ParentLocator = Target,
                Value = AddedEntityComponent,
            };

            return query.Run(world);
        }
    }
}