using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     コンポーネントを追加するアクション
    /// </summary>
    public record AddComponentAction(
        [NotNull] IAttachableID TargetAttachableID,
        [NotNull] Component Component
    ) : Action<AddComponentResult>
    {
        public override AddComponentResult Validate(IActionContext context)
        {
            return new SucceedAddComponentResult(Component);
        }
    }
}
