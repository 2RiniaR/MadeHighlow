using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーにカードを供給するアクション
    /// </summary>
    public record SupplyCardAction([NotNull] Card InitialCard) : Action<SupplyCardResult>
    {
        public override SupplyCardResult Validate(IActionContext context)
        {
            var currentContext = context;

            var registerCardResult = RegisterCard(ref currentContext);
            var succeedRegisterCardResult = registerCardResult as SucceedRegisterCardResult;
            if (succeedRegisterCardResult == null)
            {
                return new FailedSupplyCardResult(InitialCard, registerCardResult, ValueList<AddComponentResult>.Empty);
            }

            var cardID = succeedRegisterCardResult.RegisteredCard.CardID;
            var addComponentResults = InitializeComponents(ref currentContext, cardID);
            if (addComponentResults.Any(result => result is SucceedAddComponentResult == false))
            {
                return new FailedSupplyCardResult(InitialCard, registerCardResult, addComponentResults);
            }

            var interrupts = CollectInterrupts(context).Sort();
            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is RejectSupplyCardEffect)
                {
                    return new RejectedSupplyCardResult(InitialCard, interrupt.ComponentID);
                }
            }

            // `RegisterCard` アクション実行後に、副作用で対象のカードが削除される可能性があるため、存在をチェックする
            var generatedCard = cardID.GetFrom(currentContext.World);
            if (generatedCard == null)
            {
                return new FailedSupplyCardResult(InitialCard, registerCardResult, addComponentResults);
            }

            return new SucceedSupplyCardResult(InitialCard, registerCardResult, addComponentResults, generatedCard);
        }

        [NotNull]
        private RegisterCardResult RegisterCard([NotNull] ref IActionContext currentContext)
        {
            var registerCardResult = new RegisterCardAction(InitialCard).Validate(currentContext);
            currentContext = currentContext.Appended(registerCardResult);
            return registerCardResult;
        }

        [NotNull]
        [ItemNotNull]
        private ValueList<AddComponentResult> InitializeComponents(
            [NotNull] ref IActionContext currentContext,
            [NotNull] CardID entityID
        )
        {
            var addComponentResults = new List<AddComponentResult>();
            foreach (var component in InitialCard.Components)
            {
                var result = new AddComponentAction(entityID, component).Validate(currentContext);
                currentContext = currentContext.Appended(result);
                addComponentResults.Add(result);
            }

            return addComponentResults.ToValueList();
        }

        [ItemNotNull]
        [NotNull]
        private ValueList<Interrupt<SupplyCardEffect>> CollectInterrupts([NotNull] IActionContext context)
        {
            var effectors = Component.GetAllOfTypeFrom<ISupplyCardEffector>(context.World);
            return effectors.SelectMany(effector => effector.EffectsOnSupplyCard(context, this));
        }
    }
}
