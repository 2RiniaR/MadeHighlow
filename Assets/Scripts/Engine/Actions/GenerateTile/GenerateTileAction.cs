using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record GenerateTileAction([NotNull] Tile Status) : Action<GenerateTileResult>
    {
        public override GenerateTileResult Evaluate(IActionContext context)
        {
            var processRunner = new ProcessRunner(this, ref context);
            if (!processRunner.TryProcess())
            {
                return new ProcessFailedResult(Status, processRunner.Failed ?? throw new NullReferenceException());
            }

            var process = processRunner.Succeed ?? throw new NullReferenceException();

            var interrupts = CollectInterrupts(context).Sort();
            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Status, interrupt.ComponentID, process, interrupts);
                }
            }

            // `RegisterTile` アクション実行後に、副作用で対象のエンティティが削除される可能性がある
            var tileID = process.RegisterTile.RegisteredTile.TileID;
            var tile = tileID.GetFrom(context.World);
            if (tile == null)
            {
                return new FailedResult(Status, process, interrupts, FailedReason.TargetDestroyed);
            }

            return new SucceedResult(Status, tile, process, interrupts);
        }

        [ItemNotNull]
        [NotNull]
        private ValueList<Interrupt<GenerateTileEffect>> CollectInterrupts([NotNull] IActionContext context)
        {
            var effectors = Component.GetAllOfTypeFrom<IGenerateTileEffector>(context.World);
            return effectors.SelectMany(effector => effector.EffectsOnGenerateTile(context, this));
        }
    }
}
