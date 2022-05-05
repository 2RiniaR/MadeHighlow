using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     コンポーネントを追加するアクション
    /// </summary>
    public record AddComponentAction : IValidatable
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

        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public AddComponentResult Validate([NotNull] in IActionContext context)
        {
            return new AddComponentResult { AddedComponent = Component };
        }
    }
}