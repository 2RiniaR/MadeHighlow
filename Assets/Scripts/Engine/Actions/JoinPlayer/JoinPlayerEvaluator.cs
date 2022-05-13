using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer;
using RineaR.MadeHighlow.Actions.SupplyCard;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public class JoinPlayerEvaluator
    {
        public JoinPlayerEvaluator([NotNull] IActionContext context, [NotNull] Player initialStatus)
        {
            Context = context;
            InitialStatus = initialStatus;
        }

        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private Player InitialStatus { get; }

        [CanBeNull] private RegisterPlayer.SucceedResult RegisterPlayerResult { get; set; }
        [CanBeNull] private ValueList<AddComponent.SucceedResult> AddComponentResults { get; set; }
        [CanBeNull] private ValueList<SupplyCard.SucceedResult> SupplyCardResults { get; set; }

        [CanBeNull] private Player Generating { get; set; }

        [NotNull]
        public JoinPlayerResult Evaluate()
        {
            Contract.Ensures(Contract.Result<JoinPlayerResult>() != null);

            JoinPlayerResult result;

            result = RegisterPlayer();
            if (result != null) return result;

            result = AddComponents();
            if (result != null) return result;

            result = SupplyCards();
            if (result != null) return result;

            result = GetPlayer();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private JoinPlayerResult RegisterPlayer()
        {
            var result = new RegisterPlayerAction(InitialStatus).Evaluate(Context);
            if (result is not RegisterPlayer.SucceedResult succeedResult)
            {
                return new RegisterFailedResult(InitialStatus, result);
            }

            Context = Context.Appended(succeedResult);
            RegisterPlayerResult = succeedResult;

            return null;
        }

        [CanBeNull]
        private JoinPlayerResult AddComponents()
        {
            Contract.Requires<InvalidOperationException>(RegisterPlayerResult != null);

            var generatingID = RegisterPlayerResult.Registered.PlayerID;

            AddComponentResults = ValueList<AddComponent.SucceedResult>.Empty;
            foreach (var component in InitialStatus.Components)
            {
                var result = new AddComponentAction(generatingID, component).Evaluate(Context);
                if (result is not AddComponent.SucceedResult succeedResult)
                {
                    return new AddComponentFailedResult(
                        InitialStatus,
                        RegisterPlayerResult,
                        AddComponentResults,
                        result
                    );
                }

                Context = Context.Appended(succeedResult);
                AddComponentResults = AddComponentResults.Add(succeedResult);
            }

            return null;
        }

        [CanBeNull]
        private JoinPlayerResult SupplyCards()
        {
            Contract.Requires<InvalidOperationException>(RegisterPlayerResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);

            var generatingID = RegisterPlayerResult.Registered.PlayerID;

            SupplyCardResults = ValueList<SupplyCard.SucceedResult>.Empty;
            foreach (var card in InitialStatus.Cards)
            {
                var result = new SupplyCardAction(generatingID, card).Evaluate(Context);
                if (result is not SupplyCard.SucceedResult succeedResult)
                {
                    return new SupplyCardFailedResult(
                        InitialStatus,
                        RegisterPlayerResult,
                        AddComponentResults,
                        SupplyCardResults,
                        result
                    );
                }

                Context = Context.Appended(succeedResult);
                SupplyCardResults = SupplyCardResults.Add(succeedResult);
            }

            return null;
        }

        [CanBeNull]
        private JoinPlayerResult GetPlayer()
        {
            Contract.Requires<InvalidOperationException>(RegisterPlayerResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(SupplyCardResults != null);

            var generatingID = RegisterPlayerResult.Registered.PlayerID;

            // `RegisterPlayer` アクション実行後に、副作用で対象のエンティティが削除される可能性がある
            Generating = generatingID.GetFrom(Context.World);
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
            Contract.Requires<ArgumentNullException>(Generating != null);

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
