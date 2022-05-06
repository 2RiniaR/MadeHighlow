using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     エンティティを新規登録するアクション
    /// </summary>
    public record RegisterEntityAction(
        [NotNull] in Position3D Position3D,
        [NotNull] in Direction3D Direction3D,
        [CanBeNull] in Vitality Vitality
    ) : Action<RegisterEntityResult>
    {
        public override RegisterEntityResult Validate(in IActionContext context)
        {
            return new RegisterEntityResult(
                new Entity(
                    new AllocateIDAction().Validate(context).AllocatedID,
                    Components: ValueObjectList<Component>.Empty,
                    Vitality: Vitality,
                    Position3D: Position3D,
                    Direction3D: Direction3D
                )
            );
        }
    }
}