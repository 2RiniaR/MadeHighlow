using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     セッション
    /// </summary>
    public record Session(Guid ID, [NotNull] Version EngineVersion, [NotNull] [ItemNotNull] ValueList<Event> Events);
}
