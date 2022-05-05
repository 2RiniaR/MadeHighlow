using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットが受けている命令
    /// </summary>
    public abstract record UnitOperation
    {
        /// <summary>
        ///     命令を受けているユニットのID
        /// </summary>
        [NotNull]
        public UnitEnsuredID UnitID { get; init; } = new();

        /// <summary>
        ///     命令に使用しているカードのID
        /// </summary>
        [NotNull]
        public CardEnsuredID CardID { get; init; } = new();

        /// <summary>
        ///     中身のない命令
        /// </summary>
        public static UnitOperation Empty => new EmptyImpl();

        /// <summary>
        ///     命令から生成されるアクションを返す
        /// </summary>
        [NotNull]
        public abstract Action ActionIn([NotNull] in IActionContext context);

        private record EmptyImpl : UnitOperation
        {
            public override Action ActionIn(in IActionContext context)
            {
                return Action.Empty;
            }
        }
    }

    public sealed record UnitOperation<TCommand> : UnitOperation where TCommand : Command<TCommand>
    {
        /// <summary>
        ///     命令に使用しているカードのID
        /// </summary>
        [NotNull]
        public new CardEnsuredID<TCommand> CardID { get; init; } = new();

        /// <summary>
        ///     命令時に指定している追加データ
        /// </summary>
        /// <remark>例）移動コマンドの場合、「移動経路の指定」</remark>
        [NotNull]
        public CommandOption<TCommand> Option { get; init; } = CommandOption<TCommand>.Empty;

        /// <summary>
        ///     中身のない命令
        /// </summary>
        public new static UnitOperation<TCommand> Empty => new();

        public override Action ActionIn(in IActionContext context)
        {
            var world = context.World;
            var card = CardID.GetFrom(world) ?? throw new NullReferenceException();
            return card.Command.GenerateAction(Option, UnitID, context);
        }
    }
}