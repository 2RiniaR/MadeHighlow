using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     セッション
    /// </summary>
    public record Session(
        in Guid ID,
        [NotNull] in Version EngineVersion,
        [NotNull] [ItemNotNull] in ValueObjectList<SessionEvent> Events
    );
}