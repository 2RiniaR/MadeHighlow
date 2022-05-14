using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    /// <summary>
    ///     フィールド上を歩いて1マス移動するアクション
    /// </summary>
    public record StepAction(
        [NotNull] EntityID ActorEntityID,
        [NotNull] Direction2D Direction2D,
        [NotNull] [ItemNotNull] ValueList<ValidAction> AfterActions,
        [NotNull] StepCost AvailableStepCost
    ) : Action<StepResult>
    {
        protected override StepResult EvaluateBody(IHistory history)
        {
            var world = history.World;
            var actor = ActorEntityID.GetFrom(world) ?? throw new NullReferenceException();

            var originPosition = actor.Position3D.To2D();
            var originTile = originPosition.GetTile(world);
            var destPosition = originPosition.MoveTo(Direction2D, new Distance(1));
            var destTile = destPosition.GetTile(world);

            // 移動先のタイルがない場合、移動しない
            if (destTile == null)
            {
                return new FailedStepResult();
            }

            var entities = new EntityCondition { Position2D = originPosition }.Search(world);
            foreach (var entity in entities)
            {
                var reactors = entity.Components.WhereType<IStepOutReactor>();

                foreach (var reactor in reactors)
                {
                    var reactions = reactor.OnSteppedOut(history, ActorEntityID);
                    foreach (var reaction in reactions)
                    {
                        history.Appended(reaction.Result);
                    }
                }
            }

            var cost = new StepCost(1);


            // コストオーバーだったら、移動しない
            if (AvailableStepCost.Value < cost.Value)
            {
                return new FailedStepResult();
            }

            var afterActionResults = RunAfterActions(history);

            throw new NotImplementedException();
        }

        /// <summary>
        ///     ステップ後アクションを実行する
        /// </summary>
        [NotNull]
        private ValueList<Result> RunAfterActions([NotNull] IHistory session)
        {
            throw new NotImplementedException();
        }
    }
}
