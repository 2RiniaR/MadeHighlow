using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record Result([NotNull] Action Action) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public ValueList<Interrupt<CostEffect>> CostEffects { get; init; }
        public Cost Expended { get; init; }

        public Distance ClimbDistance { get; init; }
        public ValueList<Event<MoveEntity.Result>> ClimbMoves { get; init; }

        public Event<MoveEntity.Result> ShiftMove { get; init; }

        public Distance FallDistance { get; init; }
        public ValueList<Event<MoveEntity.Result>> FallMoves { get; init; }

        public Rejection Rejection { get; init; }

        public bool IsConfirmed { get; init; }
    }
}
