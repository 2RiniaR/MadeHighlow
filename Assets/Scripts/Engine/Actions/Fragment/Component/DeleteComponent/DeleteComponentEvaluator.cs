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

        [CanBeNull] private ValueList<Interrupt<DeleteComponentEffect>> Interrupts { get; set; }

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
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IDeleteComponentEffector>(Initial.World).Sort();

            Interrupts = ValueList<Interrupt<DeleteComponentEffect>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.EffectsOnDeleteComponent(Initial, Action);
                Interrupts = Interrupts.AddRange(interrupts);
            }
        }

        [CanBeNull]
        private DeleteComponentResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Action, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [NotNull]
        private DeleteComponentResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(Action, Interrupts);
        }
    }
}
