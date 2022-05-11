using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GenerateEntityAction([NotNull] Entity InitialEntity) : Action<GenerateEntityResult>
    {
        public override GenerateEntityResult Validate(IActionContext context)
        {
            var currentContext = context;

            var registerEntityResult = RegisterEntity(ref currentContext);
            var succeedRegisterEntityResult = registerEntityResult as SucceedRegisterEntityResult;
            if (succeedRegisterEntityResult == null)
            {
                return new FailedGenerateEntityResult(InitialEntity, registerEntityResult);
            }

            var entityID = succeedRegisterEntityResult.RegisteredEntity.EntityID;
            var addComponentResults = InitializeComponents(ref currentContext, entityID);
            if (addComponentResults.Any(result => result is SucceedAddComponentResult == false))
            {
                return new FailedGenerateEntityResult(InitialEntity, registerEntityResult, addComponentResults);
            }

            var positionEntityResult = PositionEntity(ref currentContext, entityID);
            var succeedPositionEntityResult = positionEntityResult as SucceedPositionEntityResult;
            if (succeedPositionEntityResult == null)
            {
                return new FailedGenerateEntityResult(
                    InitialEntity,
                    registerEntityResult,
                    addComponentResults,
                    positionEntityResult
                );
            }

            var interrupts = CollectInterrupts(context).Sort();
            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is RejectGenerateEntityEffect)
                {
                    return new RejectedGenerateEntityResult(
                        succeedPositionEntityResult.PositionedEntity,
                        interrupt.ComponentID
                    );
                }
            }

            // `RegisterEntity` アクション実行後に、副作用で対象のエンティティが削除される可能性がある
            var generatedEntity = entityID.GetFrom(currentContext.World);
            if (generatedEntity == null)
            {
                return new FailedGenerateEntityResult(InitialEntity, registerEntityResult, addComponentResults);
            }

            return new SucceedGenerateEntityResult(
                InitialEntity,
                registerEntityResult,
                addComponentResults,
                generatedEntity
            );
        }

        [NotNull]
        private RegisterEntityResult RegisterEntity([NotNull] ref IActionContext currentContext)
        {
            var registerEntityResult = new RegisterEntityAction(InitialEntity).Validate(currentContext);
            currentContext = currentContext.Appended(registerEntityResult);
            return registerEntityResult;
        }

        [NotNull]
        [ItemNotNull]
        private ValueList<AddComponentResult> InitializeComponents(
            [NotNull] ref IActionContext currentContext,
            [NotNull] EntityID entityID
        )
        {
            var addComponentResults = new List<AddComponentResult>();
            foreach (var component in InitialEntity.Components)
            {
                var result = new AddComponentAction(entityID, component).Validate(currentContext);
                currentContext = currentContext.Appended(result);
                addComponentResults.Add(result);
            }

            return addComponentResults.ToValueList();
        }

        [NotNull]
        private PositionEntityResult PositionEntity(
            [NotNull] ref IActionContext currentContext,
            [NotNull] EntityID entityID
        )
        {
            var result = new PositionEntityAction(InitialEntity).Validate(currentContext);
            currentContext = currentContext.Appended(result);
            return result;
        }

        [ItemNotNull]
        [NotNull]
        private ValueList<Interrupt<GenerateEntityEffect>> CollectInterrupts([NotNull] IActionContext context)
        {
            var effectors = Component.GetAllOfTypeFrom<IGenerateEntityEffector>(context.World);
            return effectors.SelectMany(effector => effector.EffectsOnGenerateEntity(context, this));
        }
    }
}
