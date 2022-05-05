using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     コンポーネントを追加するアクション
    /// </summary>
    public record AddComponentAction : Action<AddComponentResult>
    {
        /// <summary>
        ///     コンポーネントを追加する対象
        /// </summary>
        public ID TargetID { get; init; } = ID.None;

        /// <summary>
        ///     追加するコンポーネント
        /// </summary>
        [NotNull]
        public Component Component { get; init; } = Component.Empty;


        [NotNull]
        public override AddComponentResult Validate([NotNull] in IActionContext context)
        {
            return new AddComponentResult { AddedComponent = Component };
        }
    }
}