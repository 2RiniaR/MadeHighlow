using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record Interrupt([NotNull] Priority Priority, [NotNull] ComponentID ComponentID) : IComparable<Interrupt>
    {
        public int CompareTo(Interrupt other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Priority.CompareTo(other.Priority);
        }
    }

    /// <remarks>`Sort`を行うと、`Priority`で指定した優先度順に並び替えが行われる</remarks>
    public record Interrupt<TContent>(
        [NotNull] Priority Priority,
        [NotNull] ComponentID ComponentID,
        [NotNull] TContent Content
    ) : Interrupt(Priority, ComponentID);
}
