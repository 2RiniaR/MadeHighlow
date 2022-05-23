using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record KnockBackReduction(int Value)
    {
        public int Value { get; } = Math.Max(0, Value);

        [NotNull]
        public KnockBack Caused([NotNull] KnockBack before)
        {
            return new KnockBack(before.Direction, new Distance(before.Distance.Value - Value));
        }
    }
}
