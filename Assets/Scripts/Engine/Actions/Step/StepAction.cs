using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     オブジェクトがフィールド上を歩いて1マス移動するアクション
    /// </summary>
    public record StepAction() : Action(ActionType.Step)
    {
        /// <summary>
        ///     行動するユニット
        /// </summary>
        [NotNull]
        public EntityLocator Actor { get; init; } = new();

        /// <summary>
        ///     方向
        /// </summary>
        [NotNull]
        public Direction2D Direction2D { get; init; } = new();

        /// <summary>
        ///     追加効果
        /// </summary>
        [NotNull]
        public ValueObjectList<Action> AfterActions { get; init; } = ValueObjectList<Action>.Empty;

        /// <summary>
        ///     使用可能な移動コスト
        /// </summary>
        [NotNull]
        public StepCost AvailableCost { get; init; } = new();

        [NotNull]
        public StepResult Run([NotNull] in ISessionModel session)
        {
            var world = session.Current();
            var actor = new GetEntityQuery { Locator = Actor }.Run(world);

            var originPosition = actor.Position3D.To2D();
            var originTile = new GetTileByPositionQuery { Position2D = originPosition }.Run(world);
            var destPosition = originPosition.MoveTo(Direction2D, new Distance(1));
            var destTile = new GetTileByPositionQuery { Position2D = destPosition }.Run(world);

            // 移動先のタイルがない場合、移動しない
            if (destTile == null) return FailedStepResult.NoEntry;

            var entities = new GetMultiEntitiesQuery { Position2D = originPosition }.Run(world);
            foreach (var entity in entities)
            {
                var entityLocator = new EntityLocator { EntityID = entity.ID };
                var reactors = entity.Components.WhereType<IStepOutReactor>();

                foreach (var reactor in reactors)
                {
                    var reactions = reactor.OnSteppedOut(session, Actor, entityLocator);
                    foreach (var reaction in reactions) session.Advance(reaction.Result);
                }
            }

            var cost = new StepCost(1);


            // コストオーバーだったら、移動しない
            if (AvailableCost.Value < cost.Value) return FailedStepResult.CostOver;

            var afterActionResults = RunAfterActions(session);

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
        private ValueObjectList<Result> RunAfterActions([NotNull] in ISessionModel session)
        {
            throw new NotImplementedException();
        }
    }
}