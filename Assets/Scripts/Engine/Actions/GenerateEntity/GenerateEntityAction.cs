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

            var registerEntityResult = RegisterNewEntity(ref currentContext);
            var succeedRegisterEntityResult = registerEntityResult as SucceedRegisterEntityResult;
            if (succeedRegisterEntityResult == null)
            {
                return new FailedGenerateEntityResult(
                    InitialEntity,
                    registerEntityResult,
                    ValueList<AddComponentResult>.Empty
                );
            }

            var entityID = succeedRegisterEntityResult.RegisteredEntity.EntityID;
            var addComponentResults = InitializeComponents(ref currentContext, entityID);
            if (addComponentResults.Any(result => result is SucceedAddComponentResult == false))
            {
                return new FailedGenerateEntityResult(InitialEntity, registerEntityResult, addComponentResults);
            }

            var generatedEntity = entityID.GetFrom(currentContext.World);

            // `RegisterEntity` アクション実行後に、副作用で対象のエンティティが削除される可能性がある
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
        private RegisterEntityResult RegisterNewEntity([NotNull] ref IActionContext currentContext)
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
    }
}
