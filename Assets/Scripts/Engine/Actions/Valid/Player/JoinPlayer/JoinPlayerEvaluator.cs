using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreatePlayer;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public class JoinPlayerEvaluator
    {
        public JoinPlayerEvaluator([NotNull] IHistory initial, JoinPlayerAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private JoinPlayerAction Action { get; }

        [CanBeNull] private Event<CreatePlayer.SucceedResult> CreatePlayerEvent { get; set; }
        [CanBeNull] private JoinPlayerProcess Process { get; set; }

        [NotNull]
        public JoinPlayerResult Evaluate()
        {
            JoinPlayerResult result;

            result = CreatePlayer();
            if (result != null) return result;

            WrapProcess();

            return Succeed();
        }

        [CanBeNull]
        private JoinPlayerResult CreatePlayer()
        {
            Contract.Ensures((Contract.Result<JoinPlayerResult>() != null) ^ (CreatePlayerEvent != null));

            var result = new CreatePlayerAction(Action.InitialPlayer).Evaluate(Simulating);
            if (result is not CreatePlayer.SucceedResult succeedResult)
            {
                return new CreatePlayerFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            CreatePlayerEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(CreatePlayerEvent != null);
            Contract.Ensures(Process != null);

            Process = new JoinPlayerProcess(CreatePlayerEvent);
        }

        [NotNull]
        private JoinPlayerResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);

            return new SucceedResult(Action, Process);
        }
    }
}
