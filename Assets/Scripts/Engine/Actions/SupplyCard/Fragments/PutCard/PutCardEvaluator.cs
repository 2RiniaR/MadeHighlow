using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DropCard;

namespace RineaR.MadeHighlow.Actions.SupplyCard.PutCard
{
    public class PutCardEvaluator
    {
        public PutCardEvaluator([NotNull] IActionContext context, [NotNull] CardID targetID)
        {
            Context = context;
            TargetID = targetID;
        }

        [NotNull] private IActionContext Context { get; }
        [NotNull] private CardID TargetID { get; }

        [CanBeNull] private ValueList<Interrupt<PutCardEffect>> Interrupts { get; set; }
        [CanBeNull] private Card Target { get; set; }
        [CanBeNull] private Player Parent { get; set; }

        [NotNull]
        public PutCardResult Evaluate()
        {
            PutCardResult result;

            result = GetTarget();
            if (result != null) return result;

            result = PutInDeckIfSpaceExist();
            if (result != null) return result;

            result = TryReplaceCard();
            if (result != null) return result;

            return Overflowed();
        }

        [CanBeNull]
        private PutCardResult GetTarget()
        {
            Contract.Ensures((Contract.Result<PutCardResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(Context.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(TargetID);
            }

            return null;
        }

        [CanBeNull]
        private PutCardResult PutInDeckIfSpaceExist()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(Parent != null);

            Parent = Target.OwnerPlayerID.GetFrom(Context.World) ?? throw new NullReferenceException();
            if (!CanBePut(Parent))
            {
                return new SucceedResult(Target);
            }

            return null;
        }

        private static bool CanBePut(Player player)
        {
            return player.Cards.Count < player.DeckSize.Value;
        }

        [CanBeNull]
        private PutCardResult TryReplaceCard()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(Parent != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IPutCardEffector>(Context.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnPutCard(Context, Parent, Target)).Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is ReplaceEffect replace)
                {
                    var result = TryDropCard(replace.ReplacedID);
                    if (result != null) return result;
                }
            }

            return null;
        }

        [CanBeNull]
        private PutCardResult TryDropCard([NotNull] CardID dropID)
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(Parent != null);

            if (dropID == TargetID || Parent.Cards.Find(card => card.CardID == dropID) == null)
            {
                return null;
            }

            var result = new DropCardAction(dropID).Evaluate(Context);
            var succeedResult = result.BodyAs<DropCard.SucceedResult>();
            if (succeedResult != null)
            {
                return new ReplacedResult(Target, succeedResult);
            }

            return null;
        }

        [NotNull]
        private PutCardResult Overflowed()
        {
            Contract.Requires<InvalidOperationException>(Target != null);

            return new OverflowedResult(Target);
        }
    }
}
