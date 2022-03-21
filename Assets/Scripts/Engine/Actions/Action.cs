using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Events;

namespace RineaR.MadeHighlow.Engine.Actions
{
    /// <summary>
    ///     ゲームの状態に影響を及ぼす「行動」
    /// </summary>
    /// <param name="Type">「行動」の種別</param>
    public abstract record Action(ActionType Type)
    {
        /// <summary>
        ///     「行動」の種別
        /// </summary>
        public ActionType Type { get; } = Type;

        [NotNull]
        public abstract EventTimeline Run([NotNull] in Session session);
    }
}