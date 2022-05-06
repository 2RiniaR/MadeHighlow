using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーを新規登録するアクション
    /// </summary>
    public record RegisterEntityAction : Action<RegisterEntityResult>
    {
        /// <summary>
        ///     位置
        /// </summary>
        [NotNull]
        public Position3D Position3D { get; init; } = Position3D.Zero;

        /// <summary>
        ///     方向
        /// </summary>
        [NotNull]
        public Direction3D Direction3D { get; init; } = Direction3D.XPositive;

        /// <summary>
        ///     生命力
        /// </summary>
        [CanBeNull]
        public EntityVitality Vitality { get; init; }

        public override RegisterEntityResult Validate(in IActionContext context)
        {
            return new RegisterEntityResult
            {
                Registered = new Entity
                {
                    ID = new AllocateIDAction().Validate(context).Allocated,
                    Components = ValueObjectList<Component>.Empty,
                    Vitality = Vitality,
                    Position3D = Position3D,
                    Direction3D = Direction3D,
                },
            };
        }
    }
}