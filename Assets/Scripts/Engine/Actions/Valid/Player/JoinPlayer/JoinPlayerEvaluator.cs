using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreatePlayer;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public class JoinPlayerEvaluator
    {
        public JoinPlayerEvaluator([NotNull] ActionContext context, [NotNull] IHistory initial, JoinPlayerAction action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private ActionContext Context { get; }
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
            var result = Context.Actions.CreatePlayer(Simulating, new CreatePlayerAction(Action.InitialPlayer));
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
            Process = new JoinPlayerProcess(CreatePlayerEvent);
        }

        [NotNull]
        private JoinPlayerResult Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
