using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Actions.Command;

namespace RineaR.MadeHighlow.Directors
{
    public record PlayerSubmission
    (
        [ItemNotNull] [NotNull] ImmutableList<CommandApplication> Applications
    );
}