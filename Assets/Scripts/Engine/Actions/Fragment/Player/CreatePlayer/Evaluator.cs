using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
            Result = new Result(action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }

        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            AllocateID();
            Register();
            if (!TryCreateComponents()) return Result with { Created = null };
            return Result;
        }

        private void AllocateID()
        {
            var result = Context.Actions.AllocateID(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { AllocateID = @event };
        }

        private void Register()
        {
            var action = new RegisterPlayer.Action(Result.AllocateID.Content.Allocated, Action.InitialProps);
            var result = new RegisterPlayer.Evaluator(Context, Simulating, action).Evaluate();
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { Created = @event.Content.Registered };
        }

        private bool TryCreateComponents()
        {
            Result = Result with { CreateComponents = ValueList<Event<CreateComponent.Result>>.Empty };
            foreach (var component in Action.InitialProps.Components)
            {
                var action = new CreateComponent.Action(Result.Created.PlayerID, component);
                var result = Context.Actions.CreateComponent(Simulating, action);

                if (result.Created == null) return false;

                Simulating = Simulating.Appended(result, out var @event);
                Result = Result with
                {
                    CreateComponents = Result.CreateComponents.Add(@event),
                    Created = Result.Created with { Components = Result.Created.Components.Add(result.Created) },
                };
            }

            return true;
        }
    }
}
