using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteComponent
{
    public class DeleteComponentEvaluator
    {
        public DeleteComponentEvaluator([NotNull] IHistory initial, DeleteComponentAction action)
        {
            Initial = initial;
            Action = action;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private DeleteComponentAction Action { get; }

        [CanBeNull] private ValueList<Interrupt<DeleteComponentRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public DeleteComponentResult Evaluate()
        {
            DeleteComponentResult result;

            result = CheckTargetExist();
            if (result != null) return result;

            CollectInterrupts();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private DeleteComponentResult CheckTargetExist()
        {
            if (Action.TargetID.GetFrom(Initial.World) == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        private void CollectInterrupts()
        {
            Contract.Ensures(RejectionInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IDeleteComponentRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<DeleteComponentRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.DeleteComponentRejection(Initial, Action, RejectionInterrupts);
                RejectionInterrupts = RejectionInterrupts.Add(interrupts);
            }
        }

        [CanBeNull]
        private DeleteComponentResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private DeleteComponentResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, RejectionInterrupts);
        }
    }
}
