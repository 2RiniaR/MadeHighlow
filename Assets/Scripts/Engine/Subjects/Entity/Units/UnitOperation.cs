using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットが受けている命令
    /// </summary>
    public record UnitOperation
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
        ///     命令時に指定している追加データ
        /// </summary>
        /// <remark>例）移動コマンドの場合、「移動経路の指定」</remark>
        [NotNull]
        public CommandOption Option { get; init; } = CommandOption.Empty;
    }
}