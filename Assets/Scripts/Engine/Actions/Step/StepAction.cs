using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries.Objects;
using RineaR.MadeHighlow.Queries.Objects.Components;
using RineaR.MadeHighlow.Queries.Objects.Tiles;

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
        public ObjectLocator Actor { get; init; } = new();

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
            var actor = new GetObjectQuery { Locator = Actor }.Run(world);

            // タイルはタイルの上を移動しない
            if (actor.ObjectType == ObjectType.Tile) return FailedStepResult.InvalidActor;

            var originPosition = actor.Position2D;
            var originTile = new Get2DPositionedTileQuery { Position2D = originPosition }.Run(world);

            var destPosition = actor.Position2D.MoveTo(Direction2D, new Distance(1));
            var destTile = new Get2DPositionedTileQuery { Position2D = destPosition }.Run(world);

            // 移動先のタイルがない場合、移動しない
            if (destTile == null) return FailedStepResult.NoEntry;

            var objects = new Get2DPositionedObjectsQuery { Position2D = originPosition }.Run(world);
            foreach (var @object in objects.Items)
            {
                var objectLocator = new ObjectLocator { ObjectID = @object.ID };
                var reactors = new GetTypedComponentsQuery<IStepOutReactor>
                    { ParentLocator = objectLocator }.Run(world);

                foreach (var reactor in reactors.Items)
                {
                    var reactions = reactor.OnSteppedOut(session, Actor, objectLocator);
                    foreach (var reaction in reactions.Items) session.Advance(reaction.Result);
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