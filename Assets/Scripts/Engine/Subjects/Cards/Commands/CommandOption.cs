using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Engine.Subjects.Cards.Commands
{
    public abstract record CommandOption([NotNull] in CommandType Type);
}