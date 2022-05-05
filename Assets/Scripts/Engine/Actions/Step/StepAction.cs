using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     オブジェクトがフィールド上を歩いて1マス移動するアクション
    /// </summary>
    public record StepAction : IValidatable
    {
        /// <summary>
        ///     行動するオブジェクト
        /// </summary>
        [NotNull]
        public EntityEnsuredID Actor { get; init; } = new();

        /// <summary>
        ///     方向
        /// </summary>
        [NotNull]
        public Direction2D Direction2D { get; init; } = new();

        /// <summary>
        ///     追加効果
        /// </summary>
        [NotNull]
        public ValueObjectList<IValidatable> AfterActions { get; init; } = ValueObjectList<IValidatable>.Empty;

        /// <summary>
        ///     使用可能な移動コスト
        /// </summary>
        [NotNull]
        public StepCost AvailableCost { get; init; } = new();

        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public StepResult Validate([NotNull] in IActionContext context)
        {
            var world = context.CurrentWorld();
            var actor = Actor.Get(world) ?? throw new NullReferenceException();

            var originPosition = actor.Position3D.To2D();
            var originTile = originPosition.GetTile(world);
            var destPosition = originPosition.MoveTo(Direction2D, new Distance(1));
            var destTile = destPosition.GetTile(world);

            // 移動先のタイルがない場合、移動しない
            if (destTile == null) return FailedStepResult.NoEntry;

            var entities = new EntityCondition { Position2D = originPosition }.Search(world);
            foreach (var entity in entities)
            {
                var reactors = entity.Components.WhereType<IStepOutReactor>();

                foreach (var reactor in reactors)
                {
                    var reactions = reactor.OnSteppedOut(context, Actor);
                    foreach (var reaction in reactions) context.Append(reaction.Result);
                }
            }

            var cost = new StepCost(1);


            // コストオーバーだったら、移動しない
            if (AvailableCost.Value < cost.Value) return FailedStepResult.CostOver;

            var afterActionResults = RunAfterActions(context);

            return new SucceedStepResult
            {
                Actor = Actor,
                Direction2D = Direction2D,
                AfterActionResults = afterActionResults,
                AvailableCost = AvailableCost,
            };
        }

        /// <summary>
        ///     ステップ後アクションを実行する
        /// </summary>
        [NotNull]
        private ValueObjectList<ISimulatable> RunAfterActions([NotNull] in IActionContext session)
        {
            throw new NotImplementedException();
        }
    }
}