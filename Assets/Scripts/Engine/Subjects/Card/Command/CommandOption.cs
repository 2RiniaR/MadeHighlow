using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public abstract record CommandOption
    {
        public static CommandOption Empty => new EmptyCommandOption();
        public abstract ValueObjectList<ISimulatable> Run([NotNull] in IActionContext context, [NotNull] in Unit actor);

        private record EmptyCommandOption : CommandOption
        {
            public override ValueObjectList<ISimulatable> Run(in IActionContext context, in Unit actor)
            {
                return ValueObjectList<ISimulatable>.Empty;
            }
        }
    }
}