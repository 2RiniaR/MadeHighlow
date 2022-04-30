using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     ゲームの状態に影響を及ぼす「行動の結果」
    /// </summary>
    public abstract record Event(ActionType Type)
    {
        public ActionType Type { get; } = Type;

        [NotNull]
        public abstract World Simulate([NotNull] in World world);
    }
}