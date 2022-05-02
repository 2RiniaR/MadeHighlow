using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record Session
    {
        /// <summary>
        ///     セッションID
        /// </summary>
        public Guid ID { get; init; } = Guid.NewGuid();

        /// <summary>
        ///     セッションを実行しているエンジンのバージョン
        /// </summary>
        [NotNull]
        public Version EngineVersion { get; init; } = new();

        /// <summary>
        ///     セッションで起きたイベント
        /// </summary>
        [NotNull]
        public ValueObjectList<SessionEvent> Events { get; init; } = ValueObjectList<SessionEvent>.Empty;
    }
}