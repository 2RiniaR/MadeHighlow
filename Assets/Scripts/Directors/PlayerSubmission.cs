using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Directors
{
    public record PlayerSubmission([ItemNotNull] [NotNull] ValueObjectList<Command> Commands);
}