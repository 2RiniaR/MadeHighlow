using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            var parent = FindParent();

            if (parent == null) return Result;

            AllocateDeckSpace(parent);

            if (!IsExistDeckSpace(parent)) return Result;

            CreateCard();

            if (Result.CreateCard.Content.Created == null) return Result;

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            return Result with { IsConfirmed = true };
        }

        [CanBeNull]
        private Player FindParent()
        {
            return Context.Finder.FindPlayer(Initial.World, Action.ParentID);
        }

        private void AllocateDeckSpace([NotNull] Player parent)
        {
            if (IsExistDeckSpace(parent)) return;

            var replacers = Context.Finder.GetAllComponents<IReplacer>(Initial.World).Sort();
            var replacements = ValueList<Interrupt<Replacement>>.Empty;
            foreach (var replacer in replacers)
            {
                var context = new ReplacementContext(Initial, Result, replacements);
                var replacement = replacer.CardReplacements(context);
                if (replacement == null) continue;
                replacements = replacements.AddRange(replacement);
            }

            replacements = replacements.Sort();

            foreach (var replacement in replacements)
            {
                var action = new DropCard.Action(replacement.Content.CanReplace);
                var result = Context.Actions.DropCard(Simulating, action);

                if (result.Reacted.Content.Deleted == null) continue;

                Simulating = Simulating.Appended(result, out var @event);
                parent = Context.Finder.FindPlayer(Simulating.World, Action.ParentID);

                // 捨てたカードが「捨てられたとき、カードを3枚追加する」などの効果を持っていた時は、カードを捨てた後もデッキが埋まっている可能性がある
                if (parent == null || !IsExistDeckSpace(parent)) continue;

                Result = Result with { DropCard = @event };
                return;
            }
        }

        private static bool IsExistDeckSpace([NotNull] Player player)
        {
            return player.Cards.Count < player.DeckSize.Value;
        }

        private void CreateCard()
        {
            var action = new CreateCard.Action(Action.ParentID, Action.InitialProps);
            var result = Context.Actions.CreateCard(Simulating, action);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { CreateCard = @event };
        }
    }
}
