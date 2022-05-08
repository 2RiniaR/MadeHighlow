using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティを新規登録するアクション
    /// </summary>
    public record RegisterEntityAction(
        [NotNull] Position3D Position3D,
        [NotNull] Direction3D Direction3D,
        [CanBeNull] Vitality Vitality
    ) : Action<RegisterEntityResult>
    {
        public override RegisterEntityResult Validate(IActionContext context)
        {
            return new RegisterEntityResult(
                new Entity(
                    new AllocateIDAction().Validate(context).AllocatedID,
                    Position3D,
                    Direction3D,
                    Vitality,
                    ValueList<Component>.Empty
                )
            );
        }
    }
}
