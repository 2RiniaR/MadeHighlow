using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow
{
    public abstract record Command([NotNull] CardID CardID, [NotNull] UnitID UnitID)
    {
        [NotNull]
        public abstract Action ActionIn([NotNull] World world);
    }

    /// <summary>
    ///     引数に `TOption` を取ることで、アクションを生成できるコマンド
    /// </summary>
    public record Command<TOption>(
        [NotNull] CardID<TOption> CardID,
        [NotNull] UnitID UnitID,
        [NotNull] TOption Option
    ) : Command(CardID, UnitID)
    {
        public override Action ActionIn(World world)
        {
            return CardID.GetFrom(world).GenerateAction(Option, UnitID);
        }

        [NotNull] public new CardID<TOption> CardID { get; init; } = CardID;
    }
}
