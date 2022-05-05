using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public abstract record CommandOption
    {
        public static CommandOption Empty => new EmptyCommandOption();
        public abstract ValueObjectList<Result> Run([NotNull] in IActionContext context, [NotNull] in Unit actor);

        private record EmptyCommandOption : CommandOption
        {
            public override ValueObjectList<Result> Run(in IActionContext context, in Unit actor)
            {
                return ValueObjectList<Result>.Empty;
            }
        }
    }
}