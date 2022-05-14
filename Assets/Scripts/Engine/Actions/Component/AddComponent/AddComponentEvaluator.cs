using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.ActionFragments.RegisterComponent;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public class AddComponentEvaluator
    {
        public AddComponentEvaluator(
            [NotNull] IHistory context,
            [NotNull] IAttachableID targetID,
            [NotNull] Component initialStatus
        )
        {
            Context = context;
            TargetID = targetID;
            InitialStatus = initialStatus;
        }

        [NotNull] private IHistory Context { get; set; }
        [NotNull] private IAttachableID TargetID { get; }
        [NotNull] private Component InitialStatus { get; }

        [CanBeNull] private ActionFragments.RegisterComponent.SucceedResult RegisterComponentResult { get; set; }
        [CanBeNull] private ValueList<ReactedResult> InitializeComponentResults { get; set; }
        [CanBeNull] private ValueList<Interrupt<AddComponentEffect>> Interrupts { get; set; }
        [CanBeNull] private Component Generating { get; set; }

        [NotNull]
        public AddComponentResult Evaluate()
        {
            AddComponentResult result;

            result = RegisterComponent();
            if (result != null) return result;

            result = InitializeComponent();
            if (result != null) return result;

            result = GetGenerating();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private AddComponentResult RegisterComponent()
        {
            Contract.Ensures(
                (Contract.Result<AddComponentResult>() != null) ^
                (RegisterComponentResult != null && Generating != null)
            );

            var result = new RegisterComponentAction(TargetID, InitialStatus).Evaluate(Context);
            if (result is not ActionFragments.RegisterComponent.SucceedResult succeedResult)
            {
                return new RegisterFailedResult(TargetID, InitialStatus, result);
            }

            Context = Context.Appended(succeedResult);
            RegisterComponentResult = succeedResult;
            Generating = succeedResult.Registered;
            return null;
        }

        [CanBeNull]
        private AddComponentResult InitializeComponent()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterComponentResult != null);
            Contract.Ensures((Contract.Result<AddComponentResult>() != null) ^ (InitializeComponentResults != null));

            var actionConfirmations = Generating.InitializeActions(Context);

            InitializeComponentResults = ValueList<ReactedResult>.Empty;
            foreach (var actionConfirmation in actionConfirmations)
            {
                var result = actionConfirmation.Action.EvaluateBase(Context);
                if (!actionConfirmation.Confirmation(result))
                {
                    return new InitializeFailedResult(
                        TargetID,
                        InitialStatus,
                        RegisterComponentResult,
                        InitializeComponentResults,
                        result
                    );
                }

                InitializeComponentResults = InitializeComponentResults.Add(result);
                Context = Context.Appended(result);
            }

            return null;
        }

        [CanBeNull]
        private AddComponentResult GetGenerating()
        {
            Contract.Requires<InvalidOperationException>(RegisterComponentResult != null);
            Contract.Requires<InvalidOperationException>(InitializeComponentResults != null);
            Contract.Ensures((Contract.Result<AddComponentResult>() != null) ^ (Generating != null));

            var componentID = RegisterComponentResult.Registered.ComponentID;

            Generating = componentID.GetFrom(Context.World);
            if (Generating == null)
            {
                return new DestroyedResult(
                    TargetID,
                    InitialStatus,
                    RegisterComponentResult,
                    InitializeComponentResults
                );
            }

            return null;
        }

        [CanBeNull]
        private AddComponentResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(RegisterComponentResult != null);
            Contract.Requires<InvalidOperationException>(InitializeComponentResults != null);
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IAddComponentEffector>(Context.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnAddComponent(Context, Generating)).Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(
                        TargetID,
                        InitialStatus,
                        RegisterComponentResult,
                        InitializeComponentResults,
                        Interrupts,
                        interrupt.ComponentID
                    );
                }
            }

            return null;
        }

        [NotNull]
        private AddComponentResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RegisterComponentResult != null);
            Contract.Requires<InvalidOperationException>(InitializeComponentResults != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(Generating != null);

            return new SucceedResult(
                TargetID,
                InitialStatus,
                RegisterComponentResult,
                InitializeComponentResults,
                Interrupts,
                Generating
            );
        }
    }
}
