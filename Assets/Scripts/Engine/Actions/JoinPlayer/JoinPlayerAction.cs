using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record JoinPlayerAction : Action<JoinPlayerResult>
    {
        [NotNull] public Player Initial { get; init; } = new();

        public override JoinPlayerResult Validate(in IActionContext context)
        {
            var currentContext = context;

            var registerResult = new RegisterPlayerAction
            {
                DeckSize = Initial.DeckSize,
            }.Validate(currentContext);
            currentContext = currentContext.Appended(registerResult);

            var supplyCardResult = new SupplyCardAction
            {
                Target = registerResult.Registered.EnsuredID,
                Cards = Initial.Cards,
            }.Validate(currentContext);
            currentContext = currentContext.Appended(supplyCardResult);

            var addComponentResults = new List<AddComponentResult>();
            foreach (var component in Initial.Components)
            {
                var result = new AddComponentAction
                {
                    TargetID = registerResult.Registered.ID,
                    Component = component,
                }.Validate(currentContext);
                currentContext = currentContext.Appended(result);
            }

            throw new NotImplementedException();
        }
    }
}