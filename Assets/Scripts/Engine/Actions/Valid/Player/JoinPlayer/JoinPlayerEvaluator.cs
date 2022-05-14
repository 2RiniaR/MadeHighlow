using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterPlayer;
using RineaR.MadeHighlow.Actions.Valid.AddComponent;
using RineaR.MadeHighlow.Actions.Valid.SupplyCard;

namespace RineaR.MadeHighlow.Actions.Valid.JoinPlayer
{
    public class JoinPlayerEvaluator
    {
        public JoinPlayerEvaluator([NotNull] IHistory history, [NotNull] Player initialStatus)
        {
            History = history;
            InitialStatus = initialStatus;
        }

        [NotNull] private IHistory History { get; set; }
        [NotNull] private Player InitialStatus { get; }

        [CanBeNull] private RegisterPlayerResult RegisterPlayerResult { get; set; }
        [CanBeNull] private ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults { get; set; }
        [CanBeNull] private ValueList<ReactedResult<SupplyCard.SucceedResult>> SupplyCardResults { get; set; }

        [CanBeNull] private Player Generating { get; set; }

        [NotNull]
        public JoinPlayerResult Evaluate()
        {
            JoinPlayerResult result;

            RegisterPlayer();

            result = AddComponents();
            if (result != null) return result;

            result = SupplyCards();
            if (result != null) return result;

            result = GetPlayer();
            if (result != null) return result;

            return Succeed();
        }

        private void RegisterPlayer()
        {
            Contract.Ensures(
                (Contract.Result<JoinPlayerResult>() != null) ^ (RegisterPlayerResult != null && Generating != null)
            );

            var result = new RegisterPlayerAction(InitialStatus).Evaluate(History);
            History = History.Appended(result);
            RegisterPlayerResult = result;
            Generating = result.Registered;
        }

        [CanBeNull]
        private JoinPlayerResult AddComponents()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterPlayerResult != null);
            Contract.Ensures(AddComponentResults != null);

            AddComponentResults = ValueList<ReactedResult<AddComponent.SucceedResult>>.Empty;
            foreach (var component in InitialStatus.Components)
            {
                var result = new AddComponentAction(Generating.PlayerID, component).Evaluate(History);
                var succeedResult = result.BodyAs<AddComponent.SucceedResult>();
                if (succeedResult == null)
                {
                    return new AddComponentFailedResult(
                        InitialStatus,
                        RegisterPlayerResult,
                        AddComponentResults,
                        result
                    );
                }

                History = History.Appended(succeedResult);
                AddComponentResults = AddComponentResults.Add(succeedResult);
            }

            return null;
        }

        [CanBeNull]
        private JoinPlayerResult SupplyCards()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterPlayerResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Ensures(SupplyCardResults != null);

            SupplyCardResults = ValueList<ReactedResult<SupplyCard.SucceedResult>>.Empty;
            foreach (var card in InitialStatus.Cards)
            {
                var result = new SupplyCardAction(Generating.PlayerID, card).Evaluate(History);
                var succeedResult = result.BodyAs<SupplyCard.SucceedResult>();
                if (succeedResult == null)
                {
                    return new SupplyCardFailedResult(
                        InitialStatus,
                        RegisterPlayerResult,
                        AddComponentResults,
                        SupplyCardResults,
                        result
                    );
                }

                History = History.Appended(succeedResult);
                SupplyCardResults = SupplyCardResults.Add(succeedResult);
            }

            return null;
        }

        [CanBeNull]
        private JoinPlayerResult GetPlayer()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterPlayerResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(SupplyCardResults != null);
            Contract.Ensures((Contract.Result<JoinPlayerResult>() != null) ^ (Generating != null));

            // `RegisterPlayer` アクション実行後に、副作用で対象のエンティティが削除される可能性がある
            Generating = Generating.PlayerID.GetFrom(History.World);
            if (Generating == null)
            {
                return new DestroyedResult(InitialStatus, RegisterPlayerResult, AddComponentResults, SupplyCardResults);
            }

            return null;
        }

        [NotNull]
        private JoinPlayerResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RegisterPlayerResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(SupplyCardResults != null);
            Contract.Requires<InvalidOperationException>(Generating != null);

            return new SucceedResult(
                InitialStatus,
                RegisterPlayerResult,
                AddComponentResults,
                SupplyCardResults,
                Generating
            );
        }
    }
}
