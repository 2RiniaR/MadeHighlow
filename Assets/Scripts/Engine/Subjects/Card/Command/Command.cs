namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「カード」の指令
    /// </summary>
    public abstract record Command
    {
        /// <summary>
        ///     早さ
        /// </summary>
        public CommandQuickness Quickness { get; init; } = CommandQuickness.Last;

        public static Command None => new NoneCommand();

        private record NoneCommand : Command;
    }
}