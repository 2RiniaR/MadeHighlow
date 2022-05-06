using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     コンポーネントを追加するアクション
    /// </summary>
    public record AddComponentAction(
        [NotNull] in IAttachableID TargetAttachableID,
        [NotNull] in Component Component
    ) : Action<AddComponentResult>
    {
        public override AddComponentResult Validate(in IActionContext context)
        {
            return new SucceedAddComponentResult(Component);
        }
    }
}