using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateEntity
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
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            AllocateID();
            Register();

            if (Result.RegisterEntity.Content.Registered == null) return Result;

            CreateComponents();

            if (Result.CreateComponents.Any(@event => @event.Content.Created == null)) return Result;

            Confirm();

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
            var action = new RegisterEntity.Action(Result.AllocateID.Content.Allocated, Action.InitialProps);
            var result = Context.Actions.RegisterEntity(Simulating, action);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { RegisterEntity = @event };
        }

        private void CreateComponents()
        {
            var cardID = Result.RegisterEntity.Content.Registered.EntityID;
            Result = Result with { CreateComponents = ValueList<Event<CreateComponent.Result>>.Empty };
            foreach (var component in Action.InitialProps.Components)
            {
                var action = new CreateComponent.Action(cardID, component);
                var result = Context.Actions.CreateComponent(Simulating, action);
                Simulating = Simulating.Appended(result, out var @event);
                Result = Result with { CreateComponents = Result.CreateComponents.Add(@event) };
            }
        }

        private void Confirm()
        {
            Result = Result with
            {
                Created = Result.RegisterEntity.Content.Registered with
                {
                    Components = Result.CreateComponents.Select(@event => @event.Content.Created),
                },
            };
        }
    }
}
