using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent.RegisterComponent;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public class AddComponentEvaluator
    {
        public AddComponentEvaluator(
            [NotNull] IActionContext context,
            [NotNull] IAttachableID targetID,
            [NotNull] Component initialStatus
        )
        {
            Context = context;
            TargetID = targetID;
            InitialStatus = initialStatus;
        }

        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private IAttachableID TargetID { get; }
        [NotNull] private Component InitialStatus { get; }

        [CanBeNull] private RegisterComponent.SucceedResult RegisterComponentResult { get; set; }
        [CanBeNull] private ValueList<Result> InitializeComponentResults { get; set; }
        [CanBeNull] private ValueList<Interrupt<AddComponentEffect>> Interrupts { get; set; }
        [CanBeNull] private Component Target { get; set; }

        [NotNull]
        public AddComponentResult Evaluate()
        {
            Contract.Ensures(Contract.Result<AddComponentResult>() != null);

            AddComponentResult result;

            result = RegisterComponent();
            if (result != null) return result;

            result = InitializeComponents();
            if (result != null) return result;

            result = GetComponent();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private AddComponentResult RegisterComponent()
        {
            var result = new RegisterComponentAction(TargetID, InitialStatus).Evaluate(Context);
            if (result is not RegisterComponent.SucceedResult succeedResult)
            {
                return new RegisterFailedResult(TargetID, InitialStatus, result);
            }

            Context = Context.Appended(succeedResult);
            RegisterComponentResult = succeedResult;
            return null;
        }

        private AddComponentResult InitializeComponents()
        {
            Contract.Requires<InvalidOperationException>(RegisterComponentResult != null);

            var component = RegisterComponentResult.Registered;
            var actions = component.InitializeActions(Context);
            InitializeComponentResults = actions.Select(action => action.EvaluateAbstract(Context));
            if (!component.IsInitializeSucceed(Context, InitializeComponentResults))
            {
                return new InitializeFailedResult(
                    TargetID,
                    InitialStatus,
                    RegisterComponentResult,
                    InitializeComponentResults
                );
            }

            Context = Context.Appended(InitializeComponentResults);
            return null;
        }

        private AddComponentResult GetComponent()
        {
            Contract.Requires<InvalidOperationException>(RegisterComponentResult != null);
            Contract.Requires<InvalidOperationException>(InitializeComponentResults != null);

            var componentID = RegisterComponentResult.Registered.ComponentID;

            Target = componentID.GetFrom(Context.World);
            if (Target == null)
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

        private AddComponentResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(RegisterComponentResult != null);
            Contract.Requires<InvalidOperationException>(InitializeComponentResults != null);
            Contract.Requires<ArgumentNullException>(Target != null);

            var effectors = Component.GetAllOfTypeFrom<IAddComponentEffector>(Context.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnAddComponent(Context, Target)).Sort();
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

        private AddComponentResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RegisterComponentResult != null);
            Contract.Requires<InvalidOperationException>(InitializeComponentResults != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<ArgumentNullException>(Target != null);

            return new SucceedResult(
                TargetID,
                InitialStatus,
                RegisterComponentResult,
                InitializeComponentResults,
                Interrupts,
                Target
            );
        }
    }
}
