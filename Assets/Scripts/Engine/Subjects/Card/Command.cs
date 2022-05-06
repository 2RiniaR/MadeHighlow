using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public abstract record Command([NotNull] CardID CardID, [NotNull] UnitID UnitID)
    {
        [NotNull]
        public abstract Action ActionIn([NotNull] in World world);
    }

    public record Command<TOption>(
        [NotNull] CardID<TOption> CardID,
        [NotNull] UnitID UnitID,
        [NotNull] TOption Option
    ) : Command(CardID, UnitID)
    {
        public override Action ActionIn(in World world)
        {
            var card = CardID.GetFrom(world) ?? throw new NullReferenceException();
            return card.GenerateAction(Option, UnitID);
        }

        [NotNull] public new CardID<TOption> CardID { get; init; } = CardID;
    }
}