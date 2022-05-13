using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.GenerateEntity.PositionEntity;
using RineaR.MadeHighlow.Actions.GenerateEntity.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public class GenerateEntityEvaluator
    {
        public GenerateEntityEvaluator([NotNull] IActionContext context, [NotNull] Entity initialStatus)
        {
            Context = context;
            InitialStatus = initialStatus;
        }

        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private Entity InitialStatus { get; }

        [CanBeNull] private RegisterEntity.SucceedResult RegisterEntityResult { get; set; }
        [CanBeNull] private ValueList<AddComponent.SucceedResult> AddComponentResults { get; set; }
        [CanBeNull] private PositionEntity.SucceedResult PositionEntityResult { get; set; }
        [CanBeNull] private ValueList<Interrupt<GenerateEntityEffect>> Interrupts { get; set; }

        [CanBeNull] private Entity Generating { get; set; }

        [NotNull]
        public GenerateEntityResult Evaluate()
        {
            GenerateEntityResult result;

            result = RegisterEntity();
            if (result != null) return result;

            result = AddComponents();
            if (result != null) return result;

            result = PositionEntity();
            if (result != null) return result;

            result = GetEntity();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private GenerateEntityResult RegisterEntity()
        {
            Contract.Ensures((Contract.Result<GenerateEntityResult>() != null) ^ (RegisterEntityResult != null));

            var result = new RegisterEntityAction(InitialStatus).Evaluate(Context);
            if (result is not RegisterEntity.SucceedResult succeedResult)
            {
                return new RegisterFailedResult(InitialStatus, result);
            }

            Context = Context.Appended(succeedResult);
            RegisterEntityResult = succeedResult;

            return null;
        }

        [CanBeNull]
        private GenerateEntityResult AddComponents()
        {
            Contract.Requires<InvalidOperationException>(RegisterEntityResult != null);
            Contract.Ensures(AddComponentResults != null);

            var generatingID = RegisterEntityResult.Registered.EntityID;

            AddComponentResults = ValueList<AddComponent.SucceedResult>.Empty;
            foreach (var component in InitialStatus.Components)
            {
                var result = new AddComponentAction(generatingID, component).Evaluate(Context);
                if (result is not AddComponent.SucceedResult succeedResult)
                {
                    return new AddComponentFailedResult(
                        InitialStatus,
                        RegisterEntityResult,
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
        private GenerateEntityResult PositionEntity()
        {
            Contract.Requires<InvalidOperationException>(RegisterEntityResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Ensures((Contract.Result<GenerateEntityResult>() != null) ^ (PositionEntityResult != null));

            var generatingID = RegisterEntityResult.Registered.EntityID;

            var result = new PositionEntityAction(generatingID, InitialStatus.Position3D).Evaluate(Context);
            if (result is not PositionEntity.SucceedResult succeedResult)
            {
                return new PositionFailedResult(InitialStatus, RegisterEntityResult, AddComponentResults, result);
            }

            Context = Context.Appended(succeedResult);
            PositionEntityResult = succeedResult;

            return null;
        }

        [CanBeNull]
        private GenerateEntityResult GetEntity()
        {
            Contract.Requires<InvalidOperationException>(RegisterEntityResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(PositionEntityResult != null);
            Contract.Ensures((Contract.Result<GenerateEntityResult>() != null) ^ (Generating != null));

            var generatingID = RegisterEntityResult.Registered.EntityID;

            // `RegisterEntity` アクション実行後に、副作用で対象のエンティティが削除される可能性がある
            Generating = generatingID.GetFrom(Context.World);
            if (Generating == null)
            {
                return new DestroyedResult(
                    InitialStatus,
                    RegisterEntityResult,
                    AddComponentResults,
                    PositionEntityResult
                );
            }

            return null;
        }

        [CanBeNull]
        private GenerateEntityResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(RegisterEntityResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(PositionEntityResult != null);
            Contract.Requires<ArgumentNullException>(Generating != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IGenerateEntityEffector>(Context.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnGenerateEntity(Context, Generating)).Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(
                        InitialStatus,
                        RegisterEntityResult,
                        AddComponentResults,
                        PositionEntityResult,
                        Interrupts,
                        interrupt.ComponentID
                    );
                }
            }

            return null;
        }

        [NotNull]
        private GenerateEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RegisterEntityResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(PositionEntityResult != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<ArgumentNullException>(Generating != null);

            return new SucceedResult(
                InitialStatus,
                RegisterEntityResult,
                AddComponentResults,
                PositionEntityResult,
                Interrupts,
                Generating
            );
        }
    }
}
