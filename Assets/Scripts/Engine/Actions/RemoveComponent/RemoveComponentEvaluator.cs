using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public class RemoveComponentEvaluator
    {
        public RemoveComponentEvaluator([NotNull] IActionContext context, [NotNull] ComponentID targetID)
        {
            Context = context;
            TargetID = targetID;
        }

        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private ComponentID TargetID { get; }

        [CanBeNull] private ValueList<Result> FinalizeComponentResults { get; set; }
        [CanBeNull] private ValueList<Interrupt<RemoveComponentEffect>> Interrupts { get; set; }
        [CanBeNull] private Component Target { get; set; }

        [NotNull]
        public RemoveComponentResult Evaluate()
        {
            Contract.Ensures(Contract.Result<RemoveComponentResult>() != null);

            RemoveComponentResult result;

            result = GetComponent();
            if (result != null) return result;

            result = FinalizeComponents();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            return Succeed();
        }

        private RemoveComponentResult GetComponent()
        {
            Target = TargetID.GetFrom(Context.World);
            if (Target == null)
            {
                return new NotFoundResult(TargetID);
            }

            return null;
        }

        private RemoveComponentResult FinalizeComponents()
        {
            Contract.Requires<InvalidOperationException>(Target != null);

            var actions = Target.FinalizeActions(Context);
            FinalizeComponentResults = actions.Select(action => action.EvaluateAbstract(Context));
            if (!Target.IsFinalizeSucceed(Context, FinalizeComponentResults))
            {
                return new FinalizeFailedResult(Target, FinalizeComponentResults);
            }

            Context = Context.Appended(FinalizeComponentResults);
            return null;
        }

        private RemoveComponentResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(FinalizeComponentResults != null);
            Contract.Requires<ArgumentNullException>(Target != null);

            var effectors = Component.GetAllOfTypeFrom<IRemoveComponentEffector>(Context.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnRemoveComponent(Context, Target)).Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Target, FinalizeComponentResults, Interrupts, TargetID);
                }
            }

            return null;
        }

        private RemoveComponentResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(FinalizeComponentResults != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<ArgumentNullException>(Target != null);

            return new SucceedResult(Target, FinalizeComponentResults, Interrupts);
        }
    }
}
