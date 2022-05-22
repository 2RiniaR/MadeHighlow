using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateComponent;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public class AddComponentEvaluator
    {
        public AddComponentEvaluator([NotNull] IHistory initial, AddComponentAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private AddComponentAction Action { get; }

        [CanBeNull] private Event<CreateComponent.SucceedResult> CreateComponentEvent { get; set; }
        [CanBeNull] private AddComponentProcess Process { get; set; }

        [NotNull]
        public AddComponentResult Evaluate()
        {
            AddComponentResult result;

            result = CreateComponent();
            if (result != null) return result;

            WrapProcess();
            return Succeed();
        }

        [CanBeNull]
        private AddComponentResult CreateComponent()
        {
            var result = new CreateComponentAction(Action.TargetID, Action.InitialStatus).Evaluate(Simulating);
            if (result is not CreateComponent.SucceedResult succeedResult)
            {
                return new CreateComponentFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            CreateComponentEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(CreateComponentEvent != null);
            Contract.Ensures(Process != null);

            Process = new AddComponentProcess(CreateComponentEvent);
        }

        [NotNull]
        private AddComponentResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);

            return new SucceedResult(Action, Process);
        }
    }
}
