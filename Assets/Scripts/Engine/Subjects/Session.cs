using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     セッション
    /// </summary>
    public record Session(
        Guid ID,
        [NotNull] Version EngineVersion,
        [NotNull] [ItemNotNull] ValueObjectList<SessionEvent> Events
    );
}