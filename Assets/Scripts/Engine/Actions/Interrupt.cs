using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     コンポーネントによる、アクションへ影響を与えるの生成
    /// </summary>
    /// <remarks>`Sort`を行うと、`Priority`で指定した優先度順に並び替えが行われる</remarks>
    public record Interrupt<TEffect>(
        uint Priority,
        [NotNull] ComponentID ComponentID,
        [NotNull] TEffect Effect
    ) : IComparable<Interrupt<TEffect>>
    {
        public int CompareTo(Interrupt<TEffect> other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return Priority.CompareTo(other.Priority);
        }
    }
}
