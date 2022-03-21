using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;

namespace RineaR.MadeHighlow.Engine.Events
{
    /// <summary>
    ///     ゲームの状態に影響を及ぼす「行動の結果」
    /// </summary>
    public abstract record Event(EventType Type)
    {
        public EventType Type { get; } = Type;

        [NotNull]
        public abstract World Simulate([NotNull] in World world);
    }
}