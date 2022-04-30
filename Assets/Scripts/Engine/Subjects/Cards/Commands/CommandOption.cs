using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public abstract record CommandOption([NotNull] in CommandType Type);
}