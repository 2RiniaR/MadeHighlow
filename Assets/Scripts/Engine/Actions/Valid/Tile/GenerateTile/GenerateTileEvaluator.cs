using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PositionTile;
using RineaR.MadeHighlow.Actions.Fragment.RegisterTile;
using RineaR.MadeHighlow.Actions.Valid.AddComponent;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
{
    public class GenerateTileEvaluator
    {
        public GenerateTileEvaluator([NotNull] IHistory history, [NotNull] Tile initialStatus)
        {
            History = history;
            InitialStatus = initialStatus;
        }

        [NotNull] private IHistory History { get; set; }
        [NotNull] private Tile InitialStatus { get; }

        [CanBeNull] private RegisterTileResult RegisterTileResult { get; set; }
        [CanBeNull] private ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults { get; set; }
        [CanBeNull] private Fragment.PositionTile.SucceedResult PositionTileResult { get; set; }
        [CanBeNull] private ValueList<Interrupt<GenerateTileEffect>> Interrupts { get; set; }

        [CanBeNull] private Tile Generating { get; set; }

        [NotNull]
        public GenerateTileResult Evaluate()
        {
            GenerateTileResult result;

            RegisterTile();

            result = AddInitialComponents();
            if (result != null) return result;

            result = PositionTile();
            if (result != null) return result;

            result = GetGenerating();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            return Succeed();
        }

        private void RegisterTile()
        {
            Contract.Ensures(
                (Contract.Result<GenerateTileResult>() != null) ^ (RegisterTileResult != null && Generating != null)
            );

            var result = new RegisterTileAction(InitialStatus).Evaluate(History);
            History = History.Appended(result);
            RegisterTileResult = result;
            Generating = result.Registered;
        }

        [CanBeNull]
        private GenerateTileResult AddInitialComponents()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterTileResult != null);
            Contract.Ensures(AddComponentResults != null);

            AddComponentResults = ValueList<ReactedResult<AddComponent.SucceedResult>>.Empty;
            foreach (var component in InitialStatus.Components)
            {
                var result = new AddComponentAction(Generating.TileID, component).Evaluate(History);
                var succeedResult = result.BodyAs<AddComponent.SucceedResult>();
                if (succeedResult == null)
                {
                    return new AddComponentFailedResult(InitialStatus, RegisterTileResult, AddComponentResults, result);
                }

                History = History.Appended(succeedResult);
                AddComponentResults = AddComponentResults.Add(succeedResult);
            }

            return null;
        }

        [CanBeNull]
        private GenerateTileResult PositionTile()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterTileResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Ensures((Contract.Result<GenerateTileResult>() != null) ^ (PositionTileResult != null));

            var result = new PositionTileAction(Generating.TileID, InitialStatus.Position2D).Evaluate(History);
            if (result is not Fragment.PositionTile.SucceedResult succeedResult)
            {
                return new PositionFailedResult(InitialStatus, RegisterTileResult, AddComponentResults, result);
            }

            History = History.Appended(succeedResult);
            PositionTileResult = succeedResult;

            return null;
        }

        [CanBeNull]
        private GenerateTileResult GetGenerating()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterTileResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(PositionTileResult != null);
            Contract.Ensures((Contract.Result<GenerateTileResult>() != null) ^ (Generating != null));

            // `RegisterTile` アクション実行後に、副作用で対象のエンティティが削除される可能性がある
            Generating = Generating.TileID.GetFrom(History.World);
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
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IGenerateTileEffector>(History.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnGenerateTile(History, Generating)).Sort();
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

        [NotNull]
        private GenerateTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RegisterTileResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(PositionTileResult != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(Generating != null);

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
