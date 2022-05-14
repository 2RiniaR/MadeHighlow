using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record ActionConfirmation(ValidAction Action, [CanBeNull] Predicate<Result> Confirmation = null)
    {
        public Predicate<Result> Confirmation { get; } = Confirmation ?? (_ => true);
    }
}
