using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record JoinPlayerAction([NotNull] Player InitialPlayer) : Action<JoinPlayerResult>
    {
        public override JoinPlayerResult Validate(IActionContext context)
        {
            var currentContext = context;

            // 今のところ、プレイヤーの参加に待ったをかける要素は存在しないけど、そのうち出てくるかもしれない...
            // 「ボスがいるときはプレイヤーが新たに参加できません！」みたいな

            var registerPlayerResult = RegisterNewPlayer(ref currentContext);
            var playerID = registerPlayerResult.RegisteredPlayer.PlayerID;
            var addComponentResults = InitializeComponents(ref currentContext, playerID);
            var supplyCardResults = InitializeCards(ref currentContext, playerID);

            return new JoinPlayerResult(registerPlayerResult, addComponentResults, supplyCardResults);
        }

        [NotNull]
        private RegisterPlayerResult RegisterNewPlayer([NotNull] ref IActionContext currentContext)
        {
            var registerPlayerResult = new RegisterPlayerAction(InitialPlayer.DeckSize).Validate(currentContext);
            currentContext = currentContext.Appended(registerPlayerResult);
            return registerPlayerResult;
        }

        [NotNull]
        [ItemNotNull]
        private ValueList<AddComponentResult> InitializeComponents(
            [NotNull] ref IActionContext currentContext,
            [NotNull] PlayerID playerID
        )
        {
            var addComponentResults = new List<AddComponentResult>();
            foreach (var component in InitialPlayer.Components)
            {
                var result = new AddComponentAction(playerID, component).Validate(currentContext);
                currentContext = currentContext.Appended(result);
                addComponentResults.Add(result);
            }

            return addComponentResults.ToValueList();
        }

        [NotNull]
        [ItemNotNull]
        private ValueList<SupplyCardResult> InitializeCards(
            [NotNull] ref IActionContext currentContext,
            [NotNull] PlayerID playerID
        )
        {
            var supplyCardResults = new List<SupplyCardResult>();
            foreach (var card in InitialPlayer.Cards)
            {
                var result = new SupplyCardAction(playerID, card).Validate(currentContext);
                currentContext = currentContext.Appended(result);
                supplyCardResults.Add(result);
            }

            return supplyCardResults.ToValueList();
        }
    }
}
