using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     `Session` のラッパーオブジェクト
    /// </summary>
    /// <remarks>ステートフルだが依存先がキャッシュのため、実質イミュータブルのように扱ってよい。</remarks>
    public interface IActionContext
    {
        /// <summary>
        ///     現在のセッション
        /// </summary>
        [NotNull]
        public Session Session { get; }

        /// <summary>
        ///     現在のワールドの状態
        /// </summary>
        [NotNull]
        public World World { get; }

        /// <summary>
        ///     特定の時点でのワールドの状態
        /// </summary>
        [NotNull]
        public World WorldAt(ID id);

        /// <summary>
        ///     セッションに追記する
        /// </summary>
        [NotNull]
        public IActionContext Appended([NotNull] Result result);

        public float GetRandom();
    }
}
