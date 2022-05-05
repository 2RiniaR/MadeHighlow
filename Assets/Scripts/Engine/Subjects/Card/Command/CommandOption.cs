using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public abstract record CommandOption<TCommand> where TCommand : Command<TCommand>
    {
        [NotNull] public static CommandOption<TCommand> Empty => new EmptyImpl();

        private record EmptyImpl : CommandOption<TCommand>;
    }
}