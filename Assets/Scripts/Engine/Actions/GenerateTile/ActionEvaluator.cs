using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.GenerateTile.PositionTile;
using RineaR.MadeHighlow.Actions.GenerateTile.RegisterTile;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public class ActionEvaluator
    {
        public ActionEvaluator([NotNull] IActionContext context, [NotNull] Tile initialStatus)
        {
            Context = context;
            InitialStatus = initialStatus;
        }

        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private Tile InitialStatus { get; }

        [CanBeNull] private RegisterTile.SucceedResult RegisterTileResult { get; set; }
        [CanBeNull] private ValueList<AddComponent.SucceedResult> AddComponentResults { get; set; }
        [CanBeNull] private PositionTile.SucceedResult PositionTileResult { get; set; }
        [CanBeNull] private ValueList<Interrupt<GenerateTileEffect>> Interrupts { get; set; }

        [CanBeNull] private Tile Generating { get; set; }

        [NotNull]
        public GenerateTileResult Evaluate()
        {
            Contract.Ensures(Contract.Result<GenerateTileResult>() != null);

            GenerateTileResult error;

            error = RegisterTile();
            if (error != null) return error;

            error = AddComponents();
            if (error != null) return error;

            error = PositionTile();
            if (error != null) return error;

            error = GetTile();
            if (error != null) return error;

            error = CollectInterrupts();
            if (error != null) return error;

            return Succeed();
        }

        [CanBeNull]
        private GenerateTileResult RegisterTile()
        {
            var result = new RegisterTileAction(InitialStatus).Evaluate(Context);
            if (result is not RegisterTile.SucceedResult succeedResult)
            {
                return new RegisterFailedResult(InitialStatus, result);
            }

            Context = Context.Appended(succeedResult);
            RegisterTileResult = succeedResult;

            return null;
        }

        [CanBeNull]
        private GenerateTileResult AddComponents()
        {
            Contract.Requires<InvalidOperationException>(RegisterTileResult != null);

            var generatingID = RegisterTileResult.Registered.TileID;

            AddComponentResults = ValueList<AddComponent.SucceedResult>.Empty;
            foreach (var component in InitialStatus.Components)
            {
                var result = new AddComponentAction(generatingID, component).Evaluate(Context);
                if (result is not AddComponent.SucceedResult succeedResult)
                {
                    return new AddComponentFailedResult(InitialStatus, RegisterTileResult, AddComponentResults, result);
                }

                Context = Context.Appended(succeedResult);
                AddComponentResults = AddComponentResults.Add(succeedResult);
            }

            return null;
        }

        [CanBeNull]
        private GenerateTileResult PositionTile()
        {
            Contract.Requires<InvalidOperationException>(RegisterTileResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);

            var generatingID = RegisterTileResult.Registered.TileID;

            var result = new PositionTileAction(generatingID, InitialStatus.Position2D).Evaluate(Context);
            if (result is not PositionTile.SucceedResult succeedResult)
            {
                return new PositionFailedResult(InitialStatus, RegisterTileResult, AddComponentResults, result);
            }

            Context = Context.Appended(succeedResult);
            PositionTileResult = succeedResult;

            return null;
        }

        [CanBeNull]
        private GenerateTileResult GetTile()
        {
            Contract.Requires<InvalidOperationException>(RegisterTileResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(PositionTileResult != null);

            var generatingID = RegisterTileResult.Registered.TileID;

            // `RegisterTile` アクション実行後に、副作用で対象のエンティティが削除される可能性がある
            Generating = generatingID.GetFrom(Context.World);
            if (Generating == null)
            {
                return new DestroyedResult(InitialStatus, RegisterTileResult, AddComponentResults, PositionTileResult);
            }

            return null;
        }

        [CanBeNull]
        private GenerateTileResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(RegisterTileResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(PositionTileResult != null);
            Contract.Requires<ArgumentNullException>(Generating != null);

            var effectors = Component.GetAllOfTypeFrom<IGenerateTileEffector>(Context.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnGenerateTile(Context, Generating)).Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(
                        InitialStatus,
                        RegisterTileResult,
                        AddComponentResults,
                        PositionTileResult,
                        Interrupts,
                        interrupt.ComponentID
                    );
                }
            }

            return null;
        }

        [CanBeNull]
        private GenerateTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RegisterTileResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(PositionTileResult != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<ArgumentNullException>(Generating != null);

            return new SucceedResult(
                InitialStatus,
                RegisterTileResult,
                AddComponentResults,
                PositionTileResult,
                Interrupts,
                Generating
            );
        }
    }
}
