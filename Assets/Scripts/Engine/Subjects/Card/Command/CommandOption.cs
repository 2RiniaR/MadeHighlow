using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public abstract record CommandOption
    {
        [NotNull] public static CommandOption Empty => new EmptyImpl();

        private record EmptyImpl : CommandOption;
    }
}