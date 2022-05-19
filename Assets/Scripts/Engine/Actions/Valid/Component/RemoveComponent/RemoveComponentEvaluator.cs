using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteComponent;

namespace RineaR.MadeHighlow.Actions.Valid.RemoveComponent
{
    public class RemoveComponentEvaluator
    {
        public RemoveComponentEvaluator([NotNull] IHistory initial, RemoveComponentAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private RemoveComponentAction Action { get; }

        [CanBeNull] private Event<Fragment.DeleteComponent.SucceedResult> DeleteComponentEvent { get; set; }
        [CanBeNull] private Process Process { get; set; }

        [NotNull]
        public RemoveComponentResult Evaluate()
        {
            RemoveComponentResult result;

            result = DeleteComponent();
            if (result != null) return result;

            WrapProcess();
            return Succeed();
        }

        [CanBeNull]
        private RemoveComponentResult DeleteComponent()
        {
            var result = new DeleteComponentAction(Action.TargetID).Evaluate(Simulating);
            if (result is not Fragment.DeleteComponent.SucceedResult succeedResult)
            {
                return new DeleteComponentFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            DeleteComponentEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(DeleteComponentEvent != null);
            Contract.Ensures(Process != null);

            Process = new Process(DeleteComponentEvent);
        }

        [NotNull]
        private RemoveComponentResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);

            return new SucceedResult(Action, Process);
        }
    }
}
