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
        public ObjectLocator ObjectLocator { get; init; } = new();

        /// <summary>
        ///     追加するコンポーネント
        /// </summary>
        [NotNull]
        public Component Component { get; init; } = new EmptyComponent();

        public Result Run(in Session session)
        {
            return new AddComponentResult { Target = ObjectLocator, AddedComponent = Component };
        }
    }
}