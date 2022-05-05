using System.Collections.Immutable;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Directors
{
    public record PlayerSubmission
    (
        [ItemNotNull] [NotNull] ImmutableList<CommandApplication> Applications
    );
}