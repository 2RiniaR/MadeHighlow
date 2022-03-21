using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Engine.Subjects.Cards.Commands
{
    /// <summary>
    ///     「カード」の指令
    /// </summary>
    /// <param name="Type">種類</param>
    public abstract record Command([NotNull] in CommandType Type)
    {
        /// <summary>
        ///     早さ
        /// </summary>
        public CommandQuickness Quickness { get; init; } = CommandQuickness.Last;

        /// <summary>種類</summary>
        [NotNull]
        public CommandType Type { get; } = Type;
    }
}