using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreateEntity;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateEntity
{
    public class GenerateEntityEvaluator
    {
        public GenerateEntityEvaluator([NotNull] IHistory initial, GenerateEntityAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private GenerateEntityAction Action { get; }

        [CanBeNull] private Event<Fragment.CreateEntity.SucceedResult> CreateEntityEvent { get; set; }
        [CanBeNull] private GenerateEntityProcess Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<GenerateEntityRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public GenerateEntityResult Evaluate()
        {
            GenerateEntityResult result;

            result = CreateTarget();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private GenerateEntityResult CreateTarget()
        {
            Contract.Ensures((Contract.Result<GenerateEntityResult>() != null) ^ (CreateEntityEvent != null));

            var result = new CreateEntityAction(Action.InitialProps).Evaluate(Simulating);
            if (result is not Fragment.CreateEntity.SucceedResult succeedResult)
            {
                return new CreateEntityFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            CreateEntityEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(CreateEntityEvent != null);
            Contract.Ensures(Process != null);

            Process = new GenerateEntityProcess(CreateEntityEvent);
        }

        [CanBeNull]
        private GenerateEntityResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(RejectionInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IGenerateEntityRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<GenerateEntityRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.GenerateEntityRejection(Simulating, Action, Process, RejectionInterrupts);
                RejectionInterrupts = RejectionInterrupts.Add(interrupts);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, Process, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private GenerateEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
