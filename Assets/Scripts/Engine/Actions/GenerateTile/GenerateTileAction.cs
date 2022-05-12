using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.GenerateTile.PositionTile;
using RineaR.MadeHighlow.Actions.GenerateTile.RegisterTile;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record GenerateTileAction([NotNull] Tile Status) : Action<GenerateTileResult>
    {
        public override GenerateTileResult Validate(IActionContext context)
        {
            var process = new RunningProcess();

            if (!TryRegisterTile(ref context, ref process, out var registerTile))
            {
                return new FailedResult(Status, process.AsFailed with { RegisterTile = registerTile });
            }

            var tileID = (process.RegisterTile ?? throw new NullReferenceException()).RegisteredTile.TileID;

            if (!TryAddComponents(ref context, ref process, tileID, out var addComponents))
            {
                return new FailedResult(Status, process.AsFailed with { AddComponents = addComponents });
            }

            if (!TryPositionTile(ref context, ref process, tileID, out var positionTile))
            {
                return new FailedResult(Status, process.AsFailed with { PositionTile = positionTile });
            }

            var interrupts = CollectInterrupts(context).Sort();
            process = process with { Interrupts = interrupts };
            var succeedProcess = process.AsSucceed;

            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Status, interrupt.ComponentID, succeedProcess);
                }
            }

            // `RegisterTile` アクション実行後に、副作用で対象のエンティティが削除される可能性がある
            var generatedTile = tileID.GetFrom(context.World);
            if (generatedTile == null)
            {
                return new FailedResult(Status, process.AsFailed);
            }

            return new SucceedResult(Status, generatedTile, succeedProcess);
        }

        private bool TryRegisterTile(
            [NotNull] ref IActionContext context,
            [NotNull] ref RunningProcess process,
            [NotNull] out RegisterTileResult result
        )
        {
            result = new RegisterTileAction(Status).Validate(context);
            context = context.Appended(result);

            if (result is not RegisterTile.SucceedResult succeedResult)
            {
                return false;
            }

            process = process with { RegisterTile = succeedResult };
            return true;
        }

        private bool TryAddComponents(
            [NotNull] ref IActionContext context,
            [NotNull] ref RunningProcess process,
            [NotNull] TileID tileID,
            [NotNull] out ValueList<AddComponentResult> results
        )
        {
            results = ValueList<AddComponentResult>.Empty;

            foreach (var component in Status.Components)
            {
                var attempt = TryAddComponentItem(ref context, ref process, tileID, component, out var result);
                results = results.Add(result);
                if (!attempt)
                {
                    return false;
                }
            }

            return true;
        }

        private bool TryAddComponentItem(
            [NotNull] ref IActionContext context,
            [NotNull] ref RunningProcess process,
            [NotNull] TileID tileID,
            [NotNull] Component component,
            [NotNull] out AddComponentResult result
        )
        {
            result = new AddComponentAction(tileID, component).Validate(context);
            context = context.Appended(result);

            if (result is not AddComponent.SucceedResult succeedResult)
            {
                return false;
            }

            process = process with
            {
                AddComponents
                = (process.AddComponents ?? ValueList<AddComponent.SucceedResult>.Empty).Add(succeedResult),
            };
            return true;
        }

        private bool TryPositionTile(
            [NotNull] ref IActionContext context,
            [NotNull] ref RunningProcess process,
            [NotNull] TileID tileID,
            [NotNull] out PositionTileResult result
        )
        {
            result = new PositionTileAction(tileID, Status.Position2D).Validate(context);
            context = context.Appended(result);

            if (result is not PositionTile.SucceedResult succeedResult)
            {
                return false;
            }

            process = process with { PositionTile = succeedResult };
            return true;
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
