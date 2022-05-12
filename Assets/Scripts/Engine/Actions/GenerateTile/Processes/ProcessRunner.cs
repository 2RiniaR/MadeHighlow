using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.GenerateTile.PositionTile;
using RineaR.MadeHighlow.Actions.GenerateTile.RegisterTile;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public class ProcessRunner
    {
        public ProcessRunner([NotNull] GenerateTileAction action, ref IActionContext context)
        {
            Action = action;
            Context = context;
        }

        [NotNull] private GenerateTileAction Action { get; }
        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private RunningProcess Process { get; set; } = new();

        [CanBeNull] public FailedProcess Failed { get; private set; }
        [CanBeNull] public SucceedProcess Succeed { get; private set; }

        public bool TryProcess()
        {
            if (!TryRegisterTile(out var registerTile))
            {
                Failed = Process.AsFailed with { RegisterTile = registerTile };
                return false;
            }

            var tileID = (Process.RegisterTile ?? throw new NullReferenceException()).RegisteredTile.TileID;

            if (!TryAddComponents(tileID, out var addComponents))
            {
                Failed = Process.AsFailed with { AddComponents = addComponents };
                return false;
            }

            if (!TryPositionTile(tileID, out var positionTile))
            {
                Failed = Process.AsFailed with { PositionTile = positionTile };
                return false;
            }

            Succeed = Process.AsSucceed;
            return true;
        }

        private bool TryRegisterTile([NotNull] out RegisterTileResult result)
        {
            result = new RegisterTileAction(Action.Status).Validate(Context);
            Context = Context.Appended(result);

            if (result is not RegisterTile.SucceedResult succeedResult)
            {
                return false;
            }

            Process = Process with { RegisterTile = succeedResult };
            return true;
        }

        private bool TryAddComponents([NotNull] TileID tileID, [NotNull] out ValueList<AddComponentResult> results)
        {
            results = ValueList<AddComponentResult>.Empty;

            foreach (var component in Action.Status.Components)
            {
                var attempt = TryAddComponentItem(tileID, component, out var result);
                results = results.Add(result);
                if (!attempt)
                {
                    return false;
                }
            }

            return true;
        }

        private bool TryAddComponentItem(
            [NotNull] TileID tileID,
            [NotNull] Component component,
            [NotNull] out AddComponentResult result
        )
        {
            result = new AddComponentAction(tileID, component).Validate(Context);
            Context = Context.Appended(result);

            if (result is not AddComponent.SucceedResult succeedResult)
            {
                return false;
            }

            Process = Process with
            {
                AddComponents
                = (Process.AddComponents ?? ValueList<AddComponent.SucceedResult>.Empty).Add(succeedResult),
            };
            return true;
        }

        private bool TryPositionTile([NotNull] TileID tileID, [NotNull] out PositionTileResult result)
        {
            result = new PositionTileAction(tileID, Action.Status.Position2D).Validate(Context);
            Context = Context.Appended(result);

            if (result is not PositionTile.SucceedResult succeedResult)
            {
                return false;
            }

            Process = Process with { PositionTile = succeedResult };
            return true;
        }
    }
}
