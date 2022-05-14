using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.ActionFragments.PositionEntity;
using RineaR.MadeHighlow.ActionFragments.RegisterEntity;
using RineaR.MadeHighlow.Actions.AddComponent;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public class GenerateEntityEvaluator
    {
        public GenerateEntityEvaluator([NotNull] IHistory history, [NotNull] Entity initialStatus)
        {
            History = history;
            InitialStatus = initialStatus;
        }

        [NotNull] private IHistory History { get; set; }
        [NotNull] private Entity InitialStatus { get; }

        [CanBeNull] private RegisterEntityResult RegisterEntityResult { get; set; }
        [CanBeNull] private ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults { get; set; }
        [CanBeNull] private ActionFragments.PositionEntity.SucceedResult PositionEntityResult { get; set; }
        [CanBeNull] private ValueList<Interrupt<GenerateEntityEffect>> Interrupts { get; set; }

        [CanBeNull] private Entity Generating { get; set; }

        [NotNull]
        public GenerateEntityResult Evaluate()
        {
            GenerateEntityResult result;

            RegisterEntity();

            result = AddInitialComponents();
            if (result != null) return result;

            result = PositionEntity();
            if (result != null) return result;

            result = GetGenerating();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            return Succeed();
        }

        private void RegisterEntity()
        {
            Contract.Ensures(
                (Contract.Result<GenerateEntityResult>() != null) ^ (RegisterEntityResult != null && Generating != null)
            );

            var result = new RegisterEntityAction(InitialStatus).Evaluate(History);
            History = History.Appended(result);
            RegisterEntityResult = result;
            Generating = result.Registered;
        }

        [CanBeNull]
        private GenerateEntityResult AddInitialComponents()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterEntityResult != null);
            Contract.Ensures(AddComponentResults != null);

            AddComponentResults = ValueList<ReactedResult<AddComponent.SucceedResult>>.Empty;
            foreach (var component in InitialStatus.Components)
            {
                var result = new AddComponentAction(Generating.EntityID, component).Evaluate(History);
                var succeedResult = result.BodyAs<AddComponent.SucceedResult>();
                if (succeedResult == null)
                {
                    return new AddComponentFailedResult(
                        InitialStatus,
                        RegisterEntityResult,
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
        private GenerateEntityResult PositionEntity()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterEntityResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Ensures((Contract.Result<GenerateEntityResult>() != null) ^ (PositionEntityResult != null));

            var result = new PositionEntityAction(Generating.EntityID, InitialStatus.Position3D).Evaluate(History);
            if (result is not ActionFragments.PositionEntity.SucceedResult succeedResult)
            {
                return new PositionFailedResult(InitialStatus, RegisterEntityResult, AddComponentResults, result);
            }

            History = History.Appended(succeedResult);
            PositionEntityResult = succeedResult;

            return null;
        }

        [CanBeNull]
        private GenerateEntityResult GetGenerating()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterEntityResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(PositionEntityResult != null);
            Contract.Ensures((Contract.Result<GenerateEntityResult>() != null) ^ (Generating != null));

            // `RegisterEntity` アクション実行後に、副作用で対象のエンティティが削除される可能性がある
            Generating = Generating.EntityID.GetFrom(History.World);
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
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IGenerateEntityEffector>(History.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnGenerateEntity(History, Generating)).Sort();
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
            Contract.Requires<InvalidOperationException>(Generating != null);

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
