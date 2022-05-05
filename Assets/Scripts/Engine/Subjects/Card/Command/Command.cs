using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードによる命令
    /// </summary>
    public abstract record Command
    {
        /// <summary>
        ///     命令の早さ
        /// </summary>
        public CommandQuickness Quickness { get; init; } = CommandQuickness.Last;

        /// <summary>
        ///     空の命令
        /// </summary>
        [NotNull]
        public static Command Empty => new EmptyImpl();

        private record EmptyImpl : Command;
    }

    /// <summary>
    ///     カードによる命令
    /// </summary>
    public abstract record Command<TCommand> : Command where TCommand : Command<TCommand>
    {
        /// <summary>
        ///     空の命令
        /// </summary>
        [NotNull]
        public new static Command<TCommand> Empty => new EmptyImpl();

        /// <summary>
        ///     指定された追加データから、アクションを生成する
        /// </summary>
        [NotNull]
        public abstract Action GenerateAction(
            [NotNull] CommandOption<TCommand> option,
            [NotNull] UnitEnsuredID unitID,
            [NotNull] IActionContext context
        );

        private record EmptyImpl : Command<TCommand>
        {
            public override Action GenerateAction(
                CommandOption<TCommand> option,
                UnitEnsuredID unitID,
                IActionContext context
            )
            {
                return Action.Empty;
            }
        }
    }
}