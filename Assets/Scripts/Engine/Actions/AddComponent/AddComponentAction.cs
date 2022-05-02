using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     コンポーネントを追加するアクション
    /// </summary>
    public record AddComponentAction() : Action(ActionType.AddComponent)
    {
        /// <summary>
        ///     コンポーネントを追加する対象
        /// </summary>
        [NotNull]
        public EntityLocator EntityLocator { get; init; } = new();

        /// <summary>
        ///     追加するコンポーネント
        /// </summary>
        [NotNull]
        public EntityComponent EntityComponent { get; init; } = EntityComponent.Empty;

        public Result Run(in Session session)
        {
            return new AddComponentResult { Target = EntityLocator, AddedEntityComponent = EntityComponent };
        }
    }
}